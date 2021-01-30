using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class PlayerDeathReasonFlags
    {
        public static byte KilledViaPVP = 1;
        public static byte KilledViaNPC = 1 << 1;
        public static byte KilledViaProjectile = 1 << 2;
        public static byte KilledViaOther = 1 << 3;
        public static byte KilledViaProjectile2 = 1 << 4;
        public static byte KilledViaPVP2 = 1 << 5;
        public static byte KilledViaPVP3 = 1 << 6;
        public static byte KilledViaCustomModification = 1 << 7;

        public static byte FallDamage = 0;
        public static byte Drowning = 1;
        public static byte LavaDamage = 2;
        public static byte FallDamage2 = 3;
        public static byte DemonAltar = 4;
        public static byte CompanionCube = 6;
        public static byte Suffocation = 7;
        public static byte Burning = 8;
        public static byte PoisonVenom = 9;
        public static byte Electrified = 10;
        public static byte WoFEscape = 11;
        public static byte WoFLicked = 12;
        public static byte ChaosState = 13;
        public static byte ChaosStateV2Male = 14;
        public static byte ChaosStateV2Female = 15;
    }
}
