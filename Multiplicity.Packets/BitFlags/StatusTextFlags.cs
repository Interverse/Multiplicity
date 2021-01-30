using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class StatusTextFlags
    {
        public static byte HideStatusTextPerfcent = 1;
        public static byte StatusTextHasShadows = 2;
        public static byte ServerWantsToRunCheckBytesInClientLoopThread = 4;
    }
}
