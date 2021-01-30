using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class Zone4Flags
    {
        public static byte OldOnesArmy = 1;
        public static byte Granite = 1 << 1;
        public static byte Marble = 1 << 2;
        public static byte Hive = 1 << 3;
        public static byte GemCave = 1 << 4;
        public static byte LihzahrdTemple = 1 << 5;
        public static byte Graveyard = 1 << 6;
    }
}
