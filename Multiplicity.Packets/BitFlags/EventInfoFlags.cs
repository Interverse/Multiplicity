using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class EventInfoFlags
    {
        public static byte ShadowOrbSmashed = 1;
        public static byte DownedBoss1 = 2;
        public static byte DownedBoss2 = 4;
        public static byte DownedBoss3 = 8;
        public static byte HardMode = 16;
        public static byte DownedClown = 32;
        public static byte ServerSideCharacter = 64;
        public static byte DownedPlantBoss = 128;
    }
}
