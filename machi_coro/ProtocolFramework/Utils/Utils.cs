using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolFramework.Utils
{
    public static class Utils
    {
        public static void RegisterAllTypes()
        {
            foreach (XPacketType type in Enum.GetValues<XPacketType>())
            {
                if (type == XPacketType.Unknown) continue;

                byte packetId = (byte)type;

                XPacketTypeManager.RegisterType(type, 1, packetId);
            }
        }
    }
}
