using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class ControlFlags
    {
        public static byte ControlUp = 1;
        public static byte ControlDown = 2;
        public static byte ControlLeft = 4;
        public static byte ControlRight = 8;
        public static byte ControlJump = 16;
        public static byte ControlUseItem = 32;
        public static byte Direction = 64;
    }
}
