using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class PlayerSpawnContextFlags
    {
        public static int ReviveFromDeath = 0;
        public static int SpawningIntoWorld = 1;
        public static int RecallFromItem = 2;
    }
}
