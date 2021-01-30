using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class Zone1Flags
    {
        public static byte Dungeon = 1;
        public static byte Corruption = 1 << 1;
        public static byte Holy = 1 << 2;
        public static byte Meteor = 1 << 3;
        public static byte Jungle = 1 << 4;
        public static byte Snow = 1 << 5;
        public static byte Crimson = 1 << 6;
        public static byte WaterCandle = 1 << 7;
    }
}
