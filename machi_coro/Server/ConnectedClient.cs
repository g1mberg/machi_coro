using Game.Models;
using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Player;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Shared;

public class ConnectedClient
{
    private Socket Client { get; }

    private readonly ConcurrentQueue<byte[]> _packetSendingQueue = new();

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
                var buff = new byte[2048];
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

            case XPacketType.TradeResponse:
                ProcessTradeResponse(packet);
                break;

            case XPacketType.TradeProposal:
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
        var clients = _server.ClientsSnapshot().ToList();
        if (clients.Count != 4)
        {
            SendError("Нужно 4 игрока!");
            return;
        }
        if (!clients.All(c => c.IsReady))
        {
            SendError("Не все игроки нажали готов!");
            return;
        }
    
        Console.WriteLine($"🚀 Игрок {Username} начал игру!");

        var clientsList = clients;
        var game = new Game.Game();
        for (var i = 0; i < 4; i++)
        {
            clientsList[i].Game = game;
            clientsList[i].ClientPlayer = clientsList[i].Game.Instance.Players[i];
            clientsList[i].ClientPlayer.Name = clientsList[i].Username;
            var packet = XPacket.Create(XPacketType.YouArePlayer);
            packet.SetValue(1, i);
            clientsList[i].QueuePacketSend(packet.ToPacket());
        }
        _server.Broadcast(XPacket.Create(XPacketType.GameStart));
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
        var targetPlayerId = packet.GetValue<int>(1);
        if (targetPlayerId == -1)
        {
            instance.NextPhase(); // Steal → Change
            if (!ClientPlayer.IsChangeable && instance.Phase == Phase.Change)
                instance.NextPhase(); // Change → Build
            _server.BroadcastGameState(instance);
            return;
        }

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Steal)
        {
            SendError("Сейчас нельзя воровать");
            return;
        }

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
        instance.NextPhase(); // Steal → Change
        if (!ClientPlayer.IsChangeable && instance.Phase == Phase.Change)
            instance.NextPhase(); // Change → Build
        _server.BroadcastGameState(instance);
    }

    private void ProcessRoll(XPacket packet, bool isReroll = false)
    {
        var instance = Game.Instance;
        var dices = packet.GetValue<int>(1);

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Roll)
        {
            SendError("Сейчас нельзя кидать кубик");
            return;
        }

        if (isReroll)
        {
            if (!ClientPlayer.IsReroll)  { SendError("Нет возможности перебросить"); return; }
            if (ClientPlayer.IsRerollUsed) { SendError("Переброс уже использован"); return; }
            if (instance.DiceValue.Sum == 0) { SendError("Сначала брось кубик"); return; }
            ClientPlayer.IsRerollUsed = true;
        }
        else
        {
            // Новый ход — сбрасываем флаг
            ClientPlayer.IsRerollUsed = false;
        }

        if (dices == 2 && !ClientPlayer.IsTwoDices)
        {
            SendError("Нужен Вокзал для двух кубиков");
            return;
        }

        instance.DiceValue = PlayerAction.Roll(ClientPlayer, dices);
        _server.BroadcastGameState(instance);
    }

    private void ProcessBuild(XPacket packet)
    {
        var enterpriseName = packet.GetString(1);
        var instance = Game.Instance;

        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Build)
        {
           SendError("Сейчас нельзя строить");
           return;
        }

        // Пропуск строительства
        if (string.IsNullOrEmpty(enterpriseName))
        {
            instance.NextPhase();
            if (!(ClientPlayer.IsDoubleCheck && instance.DiceValue.IsDouble))
                instance.NextPlayer();
            instance.DiceValue = new DiceResult(0, false);
            _server.BroadcastGameState(instance);
            return;
        }

        if (!PlayerAction.TryBuild(ClientPlayer, enterpriseName, Game))
        {
            SendError("Не удалось построить");
            return;
        }

        if (ClientPlayer.HasWon())
        {
            var gameOver = XPacket.Create(XPacketType.GameOver);
            gameOver.SetString(1, ClientPlayer.Name);
            gameOver.SetValue(2, ClientPlayer.Id);
            _server.Broadcast(gameOver);
            _server.Stop();
            return;
        }
        instance.NextPhase();

        if (!(ClientPlayer.IsDoubleCheck && instance.DiceValue.IsDouble))
            instance.NextPlayer();
        instance.DiceValue = new DiceResult(0, false);
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
        if (!wants)
        {
            instance.PendingTradeFromPlayerId = -1;
            instance.NextPhase();
            _server.BroadcastGameState(instance);
            return;
        }

        var fromBuilding = packet.GetString(2);
        var toPlayerId = packet.GetValue<int>(3);
        var toBuilding = packet.GetString(4);

        var toPlayer = instance.Players.FirstOrDefault(p => p.Id == toPlayerId);
        if (toPlayer == null) { SendError("Игрок не найден"); return; }
        if (!ClientPlayer.City.Any(e => e.Name == fromBuilding)) { SendError("У тебя нет такого здания"); return; }
        if (!toPlayer.City.Any(e => e.Name == toBuilding)) { SendError("У игрока нет такого здания"); return; }

        var targetClient = _server.ClientsSnapshot().FirstOrDefault(c => c.ClientPlayer?.Id == toPlayerId);
        if (targetClient == null) { SendError("Игрок не в сети"); return; }

        instance.PendingTradeFromPlayerId = ClientPlayer.Id;
        instance.PendingTradeFromBuilding = fromBuilding;
        instance.PendingTradeToPlayerId = toPlayerId;
        instance.PendingTradeToBuilding = toBuilding;

        var proposal = XPacket.Create(XPacketType.TradeProposal);
        proposal.SetString(1, toBuilding);    // что цель отдаёт
        proposal.SetString(2, fromBuilding);  // что цель получает
        proposal.SetValue(3, ClientPlayer.Id);
        targetClient.QueuePacketSend(proposal.ToPacket());

        instance.LastAction = $"{ClientPlayer.Name} предлагает обмен {toPlayer.Name}...";
        _server.BroadcastGameState(instance);
    }

    private void ProcessTradeResponse(XPacket packet)
    {
        var instance = Game.Instance;

        if (instance.PendingTradeFromPlayerId == -1 || instance.Phase != Phase.Change)
            return;

        if (instance.PendingTradeToPlayerId != ClientPlayer.Id)
            return;

        var accepted = packet.GetValue<bool>(1);
        if (accepted)
        {
            var fromPlayer = instance.Players[instance.PendingTradeFromPlayerId];
            PlayerAction.TryChange(fromPlayer, ClientPlayer,
                instance.PendingTradeFromBuilding!, instance.PendingTradeToBuilding!);
        }

        instance.PendingTradeFromPlayerId = -1;
        instance.PendingTradeFromBuilding = null;
        instance.PendingTradeToPlayerId = -1;
        instance.PendingTradeToBuilding = null;

        instance.NextPhase(); // Change → Build
        _server.BroadcastGameState(instance);
    }

    private void ProcessConfirm()
    {
        var instance = Game.Instance;
        if (instance.CurrentPlayer != ClientPlayer || instance.Phase != Phase.Roll)
        {
            SendError("Сейчас нельзя подтвердить");
            return;
        }

        instance.NextPhase();              // Roll → Income
        ClientPlayer.IsStealer = false;
        ClientPlayer.IsChangeable = false;
        PlayerAction.Income(ClientPlayer, instance);
        instance.NextPhase();              // Income → Steal

     
        if (!ClientPlayer.IsStealer && instance.Phase == Phase.Steal)
            instance.NextPhase();          // Steal → Change

     
        if (!ClientPlayer.IsChangeable && instance.Phase == Phase.Change)
            instance.NextPhase();          // Change → Build

        _server.BroadcastGameState(instance);
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
        if (packet.Length > 2048)
        {
            throw new Exception("Max packet size is 2048 bytes.");
        }

        _packetSendingQueue.Enqueue(packet);
    }

    private void SendPackets()
    {
        while (Client.Connected)
        {
            if (_packetSendingQueue.TryDequeue(out byte[] packet))
            {
                try
                {
                    Client.Send(packet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Send error {Username}: {ex.Message}");
                    break;
                }
            }
            else
            {
                Thread.Sleep(1);
            }
        }
    }

    private void SendError(string message)
    {
        var errorPacket = XPacket.Create(XPacketType.Error);
        errorPacket.SetString(1, message);
        QueuePacketSend(errorPacket.ToPacket());
    }
}