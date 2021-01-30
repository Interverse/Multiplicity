using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class Zone3Flags
    {
        public static byte Sky = 1;
        public static byte Overworld = 1 << 1;
        public static byte DirtLayer = 1 << 2;
        public static byte RockLayer = 1 << 3;
        public static byte Underworld = 1 << 4;
        public static byte Beach = 1 << 5;
        public static byte Rain = 1 << 6;
        public static byte Sandstorm = 1 << 7;
    }
}
