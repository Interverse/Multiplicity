using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class PlayerNPCTeleportFlags
    {
        public static byte PlayerTeleport = 0;
        public static byte NPCTeleport = 1;
        public static byte PlayerTeleportToOtherPlayer = 1 << 1;
        public static byte GetPositionFromIndex = 1 << 2;
        public static byte HasExtraInfo = 1 << 3;
    }
}
