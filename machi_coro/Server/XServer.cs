using Game.Models;
using Game.Models.Dice;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using Shared;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class XServer
{
    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private readonly List<ConnectedClient> _clients = [];
    private int _nextPlayerId = 1;
    private bool _listening;
    private bool _stopListening;

    public void Start()
    {
        if (_listening) throw new Exception("Server is already listening.");
        _socket.Bind(new IPEndPoint(IPAddress.Any, 4910));
        _socket.Listen(10);
        _listening = true;
    }

    public void Stop()
    {
        if (!_listening) throw new Exception("Server is already stopped.");
        _stopListening = true;
        _socket.Shutdown(SocketShutdown.Both);
        _listening = false;
    }

    public void AcceptClients()
    {
        while (true)
        {
            if (_stopListening || _clients.Count >= 4) continue;
            
            Socket client;
            try { client = _socket.Accept(); }
            catch { return; }

            Console.WriteLine($"[!] Accepted client {_clients.Count + 1}/4 from {(IPEndPoint) client.RemoteEndPoint}");

            var id = _nextPlayerId++;
            var c = new ConnectedClient(client, this);
            lock (_clients) _clients.Add(c);
        }
    }
    
    public IEnumerable<ConnectedClient> ClientsSnapshot()
    {
        lock (_clients) return _clients.ToList();
    }

    public XPacket MakePlayerJoinedPacket(ConnectedClient c)
    {
        var p = XPacket.Create(XPacketType.PlayerJoined);
        p.SetValueRaw(1, Encoding.UTF8.GetBytes(c.Username));
        return p;
    }

    public void Broadcast(XPacket packet)
    {
        var bytes = packet.ToPacket();
        foreach (var c in ClientsSnapshot())    
            c.QueuePacketSend(bytes);
        Console.WriteLine($"SERVER: Broadcast {XPacketTypeManager.GetTypeFromPacket(packet)}");
    }

    public void BroadcastGameState(GameState state)
    {
        if (state == null)
            throw new ArgumentNullException(nameof(state));

        var packet = XPacket.Create(XPacketType.GameStateUpdate);

   
        packet.SetValue(1, state.CurrentPlayer?.Id ?? 0);
        packet.SetValue(2, (int)state.Phase);
        packet.SetValueRaw(3, DiceResult.SerializeDice(state.DiceValue));
        packet.SetString(4, state.LastAction ?? "");

  
        for (int i = 0; i < 4; i++)
        {
            var p = state.Players[i];
            packet.SetValue((byte)(10 + i), p.Money);
            packet.SetString((byte)(20 + i), p.Name ?? "");
            packet.SetString((byte)(30 + i), string.Join(",", p.City.Select(e => e.Name)));

     
            var sitesData = string.Join(",", p.Sites.Select(s => $"{s.Key}:{(s.Value.IsActivated ? 1 : 0)}"));
            packet.SetString((byte)(40 + i), sitesData);
        }

        Console.WriteLine($"BROADCAST: Player={state.CurrentPlayer?.Id} Phase={state.Phase}");
        Broadcast(packet);
    }


    public void BroadcastLobbyState()
    {
        var players = _clients.Select(c => new LobbyPlayerState
        {
            Name = c.Username,
            IsReady = c.IsReady,
        }).ToList();

        var playersData = SerializePlayers(players);

        var packet = XPacket.Create(XPacketType.LobbyState);
        packet.SetValueRaw(0, playersData);

        foreach (var client in _clients)
            client.QueuePacketSend(packet.ToPacket());
    }

    private byte[] SerializePlayers(List<LobbyPlayerState> players)
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);

        bw.Write((byte)players.Count); 

        foreach (var player in players)
        {
            var nameBytes = Encoding.UTF8.GetBytes(player.Name);
            bw.Write((byte)nameBytes.Length);
            bw.Write(nameBytes);
            bw.Write(player.IsReady);
        }

        return ms.ToArray();
    }


    public void BroadcastExcept(ConnectedClient except, XPacket packet)
    {
        var bytes = packet.ToPacket();
        foreach (var c in ClientsSnapshot())
            if (c != except) c.QueuePacketSend(bytes);
    }
}
