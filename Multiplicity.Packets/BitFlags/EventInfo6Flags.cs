using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class EventInfo6Flags
    {
        public static byte CombatBookUsed = 1;
        public static byte ManualLanterns = 2;
        public static byte DownedSolarTower = 4;
        public static byte DownedVortexTower = 8;
        public static byte DownedNebulaTower = 16;
        public static byte DownedStardustTower = 32;
        public static byte ForceHalloween = 64;
        public static byte ForceXMas = 64;
    }
}
