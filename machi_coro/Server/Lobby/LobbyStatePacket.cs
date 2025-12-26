using ProtocolFramework.Serializator;

namespace Server.Lobby
{
    public class LobbyStatePacket
    {
        [XField(0)]
        public string PlayersJson;
    }
}
