using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class Zone2Flags
    {
        public static byte PeaceCandle = 1;
        public static byte SolarTower = 1 << 1;
        public static byte VortexTower = 1 << 2;
        public static byte NebulaTower = 1 << 3;
        public static byte StardustTower = 1 << 4;
        public static byte Desert = 1 << 5;
        public static byte Glowshroom = 1 << 6;
        public static byte UndergroundDesert = 1 << 7;
    }
}
