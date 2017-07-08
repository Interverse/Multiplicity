using System.Drawing;
using System.IO;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static Color ReadColor(this BinaryReader br)
        {
            byte[] colourPayload = br.ReadBytes(3);
            return Color.FromArgb(colourPayload[0], colourPayload[1], colourPayload[2]);
        }

        public static NetworkText ReadNetworkText(this BinaryReader br)
        {
            return new NetworkText(br);
        }
    }
}

