using System.IO;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets.Extensions
{
	public static class BinaryWriterExtensions
	{
		public static void Write(this BinaryWriter bw, ColorStruct color)
		{
			bw.Write(new byte[3] { color.R, color.G, color.B }, 0, 3);
		}

        public static void Write(this BinaryWriter bw, NetworkText text)
        {
            text.ToStream(bw);
        }
	}
}

