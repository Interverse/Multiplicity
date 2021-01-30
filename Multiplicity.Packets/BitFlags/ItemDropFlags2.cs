using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class ItemDropFlags2
    {
        public static byte Width = 1;
        public static byte Height = 1 << 1;
        public static byte Scale = 1 << 2;
        public static byte Ammo = 1 << 3;
        public static byte UseAmmo = 1 << 4;
        public static byte NotAmmo = 1 << 5;
    }
}
