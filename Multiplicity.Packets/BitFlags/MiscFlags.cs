using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class MiscFlags
    {
        public static byte HoveringUp = 1;
        public static byte VoidVaultEnabled = 2;
        public static byte Sitting = 4;
        public static byte DownedDD2Event = 8;
        public static byte IsPettingAnimal = 16;
        public static byte IsPettingSmallAnimal = 32;
        public static byte UsedPotionofReturn = 64;
        public static byte HoveringDown = 128;
    }
}
