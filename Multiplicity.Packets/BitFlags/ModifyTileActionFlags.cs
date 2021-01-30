using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.BitFlags
{
    public class ModifyTileActionFlags
    {
        public static byte KillTile = 0;
        public static byte PlaceTile = 1;
        public static byte KillWall = 2;
        public static byte PlaceWall = 3;
        public static byte KillTileNoItem = 4;
        public static byte PlaceWire = 5;
        public static byte KillWire = 6;
        public static byte PoundTile = 7;
        public static byte PlaceActuator = 8;
        public static byte KillActuator = 9;
        public static byte PlaceWire2 = 10;
        public static byte KillWire2 = 11;
        public static byte PlaceWire3 = 12;
        public static byte KillWire3 = 13;
        public static byte SlopeTile = 14;
        public static byte FrameTrack = 15;
        public static byte PlaceWire4 = 16;
        public static byte KillWire4 = 17;
        public static byte PokeLogicGate = 18;
        public static byte Actuate = 19;
        public static byte KillTile2 = 20;
        public static byte ReplaceTile = 21;
        public static byte ReplaceWall = 22;
        public static byte SlopePoundTile = 23;
    }
}
