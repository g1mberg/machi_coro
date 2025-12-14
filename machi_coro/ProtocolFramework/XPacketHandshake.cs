using ProtocolFramework.Serializator;

namespace ProtocolFramework
{
    public class XPacketHandshake
    {
        [XField(1)]
        public int MagicHandshakeNumber;
    }
}