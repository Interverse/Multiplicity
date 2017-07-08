using System.IO;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets.Extensions
{
	public static class BinaryWriterExtensions
	{
		public static void Write(this BinaryWriter bw, System.Drawing.Color color)
		{
			byte[] rgb = new byte[3];

			rgb[0] = (byte)color.R;
			rgb[1] = (byte)color.G;
			rgb[2] = (byte)color.B;

			bw.Write(rgb, 0, 3);
		}

        public static void Write(this BinaryWriter bw, NetworkText text)
        {
            text.ToStream(bw);
        }
	}
}

