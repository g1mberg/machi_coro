using ProtocolFramework;
using ProtocolFramework.Serializator;

namespace Shared;

public partial class ConnectedClient
{
    private void ProcessHandshake(XPacket packet)
    {
        var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);
        handshake.MagicHandshakeNumber -= 15;
        QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
    }

    private void ProcessPlayerConnected(XPacket packet)
    {
        Username = packet.GetString(1);
        QueuePacketSend(XPacket.Create(XPacketType.Welcome).ToPacket());
        _server.BroadcastLobbyState();
    }

    private void ProcessReady()
    {
        IsReady = true;
        _server.BroadcastLobbyState();
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

        Console.WriteLine($"Игрок {Username} начал игру!");

        var game = new Game.Game();
        for (var i = 0; i < 4; i++)
        {
            clients[i].Game = game;
            clients[i].ClientPlayer = game.Instance.Players[i];
            clients[i].ClientPlayer.Name = clients[i].Username;
            var packet = XPacket.Create(XPacketType.YouArePlayer);
            packet.SetValue(1, i);
            clients[i].QueuePacketSend(packet.ToPacket());
        }
        _server.Broadcast(XPacket.Create(XPacketType.GameStart));
        _server.BroadcastGameState(game.Instance);
    }
}
