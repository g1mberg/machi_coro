using ProtocolFramework.Serializator;

namespace Server.Lobby
{
    public class LobbyState
    {
        public List<LobbyPlayerState> Players { get; set; } = new();
    }

    public class LobbyPlayerState
    {
        public string Name { get; set; } = "";
        public bool IsReady { get; set; }
    }
}
