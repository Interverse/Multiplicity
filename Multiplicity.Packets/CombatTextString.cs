using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CombatTextString (0x77) packet.
    /// </summary>
    public class CombatTextString : TerrariaPacket
    {

        public float X { get; set; }

        public float Y { get; set; }

        public ColorStruct Color { get; set; }

        public NetworkText CombatText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatTextString"/> class.
        /// </summary>
        public CombatTextString()
            : base((byte)PacketTypes.CombatTextString)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatTextString"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public CombatTextString(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
            this.Color = br.ReadColor();
            this.CombatText = br.ReadNetworkText();
        }

        public override string ToString()
        {
            return $"[CombatTextString: X = {X}, Y = {Y}, Color = {Color}, CombatText = {CombatText}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(11 + CombatText.GetLength());
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader)
            {
                base.ToStream(stream, includeHeader);
            }

            /*
             * Always make sure to not close the stream when serializing.
             * 
             * It is up to the caller to decide if the underlying stream
             * gets closed.  If this is a network stream we do not want
             * the regressions of unconditionally closing the TCP socket
             * once the payload of data has been sent to the client.
             */
            using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
            {
                br.Write(X);
                br.Write(Y);
                br.Write(Color);
                br.Write(CombatText);
            }
        }

        #endregion

    }
}
