using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets.Models
{
	/// <summary>
	/// Represents an translatable string of text
	/// </summary>
	public class NetworkText
	{
        public enum Mode : byte
        {
            Literal = 0,
            Formattable,
            LocalizationKey,
        }

		/// <summary>
		/// Mode of the text
		/// </summary>
		public byte TextMode { get; set; }

		/// <summary>
		/// The text itself
		/// </summary>
		public string Text { get; set; }

        /// <summary>
        /// The length of the SubstitutionList
        /// </summary>
        public byte SubstitutionListLength { get; set; }

		/// <summary>
		/// A list of substitutions to make
		/// </summary>
        public NetworkText[] SubstitutionList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkText"/> class.
        /// </summary>
        public NetworkText()
        {
            
        }

        /// <summary>
        /// Reads from the given reader and initializes a new instance of the <see cref="NetworkText"/> class.
        /// </summary>
        /// <param name="br">Reader to initialize instance from.</param>
        public NetworkText(BinaryReader br)
		{
            this.TextMode = br.ReadByte();
            this.Text = br.ReadString();
            if (this.TextMode != (byte) Mode.Literal)
            {
                this.SubstitutionListLength = br.ReadByte();
                this.SubstitutionList = new NetworkText[(int)SubstitutionListLength];

                for (int i = 0; i < this.SubstitutionListLength; i++)
                {
                    this.SubstitutionList[i] = br.ReadNetworkText();
                }
            }
		}

        /// <summary>
        /// Writes this instance to the given BinaryWriter.
        /// </summary>
        /// <param name="bw">BinaryWriter to write contents to.</param>
        public void ToStream(BinaryWriter bw)
		{
            bw.Write(TextMode);
            bw.Write(Text);
            if (this.TextMode != (byte) Mode.Literal)
            {
                bw.Write(SubstitutionList.Length);

                for (int i = 0; i < this.SubstitutionListLength; i++)
                {
                    bw.Write(this.SubstitutionList[i]);
                }
            }
		}

        /// <summary>
        /// Gets the length of the NetworkText object in bytes.
        /// </summary>
        /// <returns>The length in bytes.</returns>
        public short GetLength()
        {
            short length = 1;

            // Length of the text in bytes (Terraria only supports extended ASCII)
            // Add 1 to accomodate for the string length byte
            length += (short)(1 + (short)this.Text.Length);

            if (this.TextMode != (byte) Mode.Literal)
            {
                length += 1;
                for (int i = 0; i < this.SubstitutionListLength; i++)
                {
                    length += this.SubstitutionList[i].GetLength();
                }
            }

            return length;
        }
	}
}
