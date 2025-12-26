using ProtocolFramework;
using ProtocolFramework.Serializator;
using System.Net.Sockets;
using Game.Models;
using Game.Models.Enterprises;
using Game.Models.Player;

namespace Shared;

public class ConnectedClient
{
    private Socket Client { get; }

    private readonly Queue<byte[]> _packetSendingQueue = [];

    private readonly XServer _server;

    public bool IsReady= false;
    private Game.Game Game { get; set; }
    public Player ClientPlayer { get; set; }
    public string Username { get; private set; } = "";

    public ConnectedClient(Socket client, XServer server)
    {
        Client = client;
        _server = server;

        Task.Run((Action)ProcessIncomingPackets);
        Task.Run((Action)SendPackets);
    }

    private void ProcessIncomingPackets()
    {
        try
        {
            while (true)
            {
                var buff = new byte[256];
                Client.Receive(buff);

                buff = buff.TakeWhile((b, i) =>
                {
                    if (b != 0xFF) return true;
                    return buff[i + 1] != 0;
                }).Concat(new byte[] { 0xFF, 0 }).ToArray();

                var parsed = XPacket.Parse(buff);
                ProcessIncomingPacket(parsed);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("CLIENT LOOP CRASH: " + ex);
            Client.Close();
        }
    }

    private void ProcessIncomingPacket(XPacket packet)
    {
        var type = XPacketTypeManager.GetTypeFromPacket(packet);

        switch (type)
        {
            case XPacketType.Handshake:
                ProcessHandshake(packet);
                break;

            case XPacketType.PlayerConnected:
                ProcessPlayerConnected(packet);
                break;

            case XPacketType.Roll:
                ProcessRoll(packet);
                break;
            
            case XPacketType.Reroll:
                ProcessRoll(packet, true);
                break;
            
            case XPacketType.Build:
                ProcessBuild(packet);
                break;
            
            case XPacketType.Change:
                ProcessChange(packet);
                break;
            
            case XPacketType.Steal:
                ProcessSteal(packet);
                break;

            case XPacketType.Confirm:
                ProcessConfirm();
                break;
            
            case XPacketType.GameStart:
                ProcessGameStart();
                break;
            case XPacketType.PlayerReady:
                ProcessReady();
                break;

            case XPacketType.Error:
            case XPacketType.Welcome:
            case XPacketType.PlayerJoined:
            case XPacketType.Unknown:
                break;

            case XPacketType.GameStateUpdate:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void ProcessGameStart()
    {
        if (_server.ClientsSnapshot().Count() != 4)
        {
            SendError("Нужно 4 игрока!");
            return;
        }
    
        Console.WriteLine($"🚀 Игрок {Username} начал игру!");

        var clientsList = _server.ClientsSnapshot().ToList();
        var game = new Game.Game();
        for (var i = 0; i < 4; i++)
        {
            clientsList[i].Game = game;
            clientsList[i].ClientPlayer = clientsList[i].Game.Instance.Players[i];
        }
        
        _server.BroadcastGameState(clientsList[0].Game.Instance);
    }

    private void ProcessReady()
    {
        IsReady = true;
        _server.BroadcastLobbyState();
    }

    private void ProcessSteal(XPacket packet)
    {
        var instance = Game.Instance;
        
        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Steal)
        {
            SendError("Сейчас нельзя воровать");
            return;
        }
        
        var targetPlayerId = packet.GetValue<int>(1);               
        var amount = packet.GetValue<int>(2);                        

        var target = instance.Players.FirstOrDefault(p => p.Id == targetPlayerId);
        if (target is null)
        {
            SendError("Цель не найдена");
            return;
        }

        if (!PlayerAction.TrySteal(ClientPlayer, target, amount))
        {
            SendError("Украсть не получилось");
            return;
        }
        instance.NextPhase();
        _server.BroadcastGameState(instance);
    }

    private void ProcessRoll(XPacket packet, bool isReroll = false)
    {
        var instance = Game.Instance;
        var dices = packet.GetValue<int>(1);
        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Roll || (isReroll && !ClientPlayer.IsReroll))
        {
            SendError("Сейчас нельзя кидать кубик");
            return;
        }

        Game.Instance.DiceValue = PlayerAction.Roll(ClientPlayer, dices);
        _server.BroadcastGameState(instance);
    }

    private void ProcessBuild(XPacket packet)
    {
        var enterpriseName = packet.GetString(1);
        var instance = Game.Instance;

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Build)
        {
           SendError("сейчас нельзя строить");
           return;
        }

        if (!PlayerAction.TryBuild(ClientPlayer, enterpriseName, Game))
        {
            SendError("брут нельзя");
            return;
        }

        if (ClientPlayer.HasWon())
        {
            _server.Stop(); //TODO: нормально надо реализовать победу
            return;
        }
        instance.NextPhase();

        if (!(ClientPlayer.IsDoubleCheck && instance.DiceValue.IsDouble))
            instance.NextPlayer();
        _server.BroadcastGameState(instance);
    }

    private void ProcessChange(XPacket packet)
    {
        var instance = Game.Instance;
    
        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Change)
        {
            SendError("Сейчас нельзя обмениваться");
            return;
        }

        var wants = packet.GetValue<bool>(1);
        if (wants)
        {
            var fromBuilding = packet.GetString(2);
            var toPlayerId = packet.GetValue<int>(3);
            var toBuilding = packet.GetString(4);

            if (!PlayerAction.TryChange(ClientPlayer, Game.Instance.Players[toPlayerId], fromBuilding, toBuilding))
            {
                SendError("Обмен невозможен");
                return;
            }
        }

        Game.Instance.NextPhase();
        _server.BroadcastGameState(instance);
    }

    private void ProcessConfirm()
    {
        if (Game.Instance.Phase != Phase.Roll)
        {
            SendError("lol");
            return;
        }
        
        Game.Instance.NextPhase();
        PlayerAction.Income(ClientPlayer, Game.Instance);
        Game.Instance.NextPhase();
    }



    private void ProcessPlayerConnected(XPacket packet)
    {
        Username = packet.GetString(1);
        var welcome = XPacket.Create(XPacketType.Welcome);
        QueuePacketSend(welcome.ToPacket());
        _server.BroadcastLobbyState();
    }



    private void ProcessHandshake(XPacket packet)
    {
        Console.WriteLine("Recieved handshake packet.");

        var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);
        handshake.MagicHandshakeNumber -= 15;

        Console.WriteLine("Answering..");

        QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
    }

    public void QueuePacketSend(byte[] packet)
    {
        if (packet.Length > 256)
        {
            throw new Exception("Max packet size is 256 bytes.");
        }

        _packetSendingQueue.Enqueue(packet);
    }

    private void SendPackets()
    {
        while (true)
        {
            if (_packetSendingQueue.Count == 0)
            {
                Thread.Sleep(100);
                continue;
            }

            var packet = _packetSendingQueue.Dequeue(); 
            Client.Send(packet);

            Thread.Sleep(100);
        }
    }
    
    private void SendError(string message)
    {
        var errorPacket = XPacket.Create(XPacketType.Error);
        errorPacket.SetValue(1, message);
        QueuePacketSend(errorPacket.ToPacket());
    }
}