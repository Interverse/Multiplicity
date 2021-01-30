using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class NPCUpdateFlags2
    {
        public static byte StatsScaled = 1;
        public static byte SpawnedFromStatue = 1 << 1;
        public static byte StrengthMultiplier = 1 << 2;
    }
}
