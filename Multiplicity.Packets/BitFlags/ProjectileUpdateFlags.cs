using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class ProjectileUpdateFlags
    {
        public static byte AI0 = 1;
        public static byte AI1 = 1 << 1;
        public static byte BannerIdToRespondTo = 1 << 3;
        public static byte Damage = 1 << 4;
        public static byte Knockback = 1 << 5;
        public static byte OriginalDamage = 1 << 6;
        public static byte ProjUUID = 1 << 7;
    }
}
