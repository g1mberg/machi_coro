using Game.Models.Player;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Shared;

public partial class ConnectedClient
{
    private Socket Client { get; }
    private readonly ConcurrentQueue<byte[]> _packetSendingQueue = new();
    private readonly XServer _server;

    public bool IsReady = false;
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
            case XPacketType.Handshake:       ProcessHandshake(packet);      break;
            case XPacketType.PlayerConnected: ProcessPlayerConnected(packet); break;
            case XPacketType.PlayerReady:     ProcessReady();                 break;
            case XPacketType.GameStart:       ProcessGameStart();             break;
            case XPacketType.Roll:            ProcessRoll(packet);            break;
            case XPacketType.Reroll:          ProcessRoll(packet, true);      break;
            case XPacketType.Confirm:         ProcessConfirm();               break;
            case XPacketType.Build:           ProcessBuild(packet);           break;
            case XPacketType.Steal:           ProcessSteal(packet);           break;
            case XPacketType.Change:          ProcessChange(packet);          break;
            case XPacketType.TradeResponse:   ProcessTradeResponse(packet);   break;

            case XPacketType.TradeProposal:
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

    public void QueuePacketSend(byte[] packet)
    {
        if (packet.Length > 2048)
            throw new Exception("Max packet size is 2048 bytes.");

        _packetSendingQueue.Enqueue(packet);
    }

    private void SendPackets()
    {
        while (Client.Connected)
        {
            if (_packetSendingQueue.TryDequeue(out byte[] packet))
            {
                try { Client.Send(packet); }
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
