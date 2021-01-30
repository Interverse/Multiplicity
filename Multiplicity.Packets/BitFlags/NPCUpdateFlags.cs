using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class NPCUpdateFlags
    {
        public static byte None = 0;
        public static byte DirectionX = 1;
        public static byte DirectionY = 1 << 1;
        public static byte AI3 = 1 << 2;
        public static byte AI2 = 1 << 3;
        public static byte AI1 = 1 << 4;
        public static byte AI0 = 1 << 5;
        public static byte SpriteDirection = 1 << 6;
        public static byte LifeMax = 1 << 7;
    }
}
