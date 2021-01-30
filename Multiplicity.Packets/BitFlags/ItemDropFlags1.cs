using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class ItemDropFlags1
    {
        public static byte Color = 1;
        public static byte Damage = 1 << 1;
        public static byte Knockback = 1 << 2;
        public static byte UseAnimation = 1 << 3;
        public static byte UseTime = 1 << 4;
        public static byte Shoot = 1 << 5;
        public static byte ShootSpeed = 1 << 6;
        public static byte NextFlags = 1 << 7;
    }
}
