using System;
using System.Drawing;
using System.IO;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static ColorStruct ReadColor(this BinaryReader br)
        {
            byte[] colourPayload = br.ReadBytes(3);
            return new ColorStruct() { R = colourPayload[0], G = colourPayload[1], B = colourPayload[2] };
        }

        public static NetworkText ReadNetworkText(this BinaryReader br)
        {
            return new NetworkText(br);
        }
    }
}

