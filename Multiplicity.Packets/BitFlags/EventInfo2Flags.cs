using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class EventInfo2Flags
    {
        public static byte MechBossDowned = 1;
        public static byte MechBossDowned2 = 2;
        public static byte MechBossDowned3 = 4;
        public static byte MechBossAnyDowned = 8;
        public static byte CloudBG = 16;
        public static byte Crimson = 32;
        public static byte PumpkinMoon = 64;
        public static byte SnowMoon = 128;
    }
}
