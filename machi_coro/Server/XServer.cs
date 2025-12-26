using ProtocolFramework;
using System.Net;
using System.Net.Sockets;
using Game.Models;

namespace Shared;

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
            if (_stopListening || _clients.Count >= 4) return;
            
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
        p.SetValue(1, c.ClientPlayer.Id);
        p.SetValueRaw(2, System.Text.Encoding.UTF8.GetBytes(c.Username));
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
        var packet = XPacket.Create(XPacketType.GameStateUpdate);
        packet.SetValue(1, state.CurrentPlayer.Id);
        packet.SetValue(2, (int)state.Phase);
        packet.SetValue(3, state.DiceValue);
        
        Console.WriteLine($"BROADCAST: Player={state.CurrentPlayer.Id} Phase={state.Phase}");
        Broadcast(packet);
    }
    
    public void BroadcastExcept(ConnectedClient except, XPacket packet)
    {
        var bytes = packet.ToPacket();
        foreach (var c in ClientsSnapshot())
            if (c != except) c.QueuePacketSend(bytes);
    }
}
