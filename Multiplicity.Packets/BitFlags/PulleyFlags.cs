using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class PulleyFlags
    {
        public static byte PulleyEnabled = 1;
        public static byte Direction = 2;
        public static byte UpdateVelocity = 4;
        public static byte VortexStealthActive = 8;
        public static byte GravityDirection = 16;
        public static byte ShieldRaised = 32;
    }
}
