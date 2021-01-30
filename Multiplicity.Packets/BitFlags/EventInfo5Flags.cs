using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class EventInfo5Flags
    {
        public static byte DownedPirates = 1;
        public static byte DownedFrostLegion = 2;
        public static byte DownedGoblins = 4;
        public static byte Sandstorm = 8;
        public static byte DD2Event = 16;
        public static byte DownedDD2Tier1 = 32;
        public static byte DownedDD2Tier2 = 64;
        public static byte DownedDD2Tier3 = 128;
    }
}
