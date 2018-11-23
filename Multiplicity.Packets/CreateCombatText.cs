using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using System.Drawing;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CreateCombatText (0x51) packet.
    /// </summary>
    public class CreateCombatText : TerrariaPacket
    {

        public float X { get; set; }

        public float Y { get; set; }

        public ColorStruct Color { get; set; }

        public int HealAmount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCombatText"/> class.
        /// </summary>
        public CreateCombatText()
            : base((byte)PacketTypes.CreateCombatText)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCombatText"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public CreateCombatText(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
            this.Color = br.ReadColor();
            this.HealAmount = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[CreateCombatText: X = {X} Y = {Y} Color = {Color} HealAmount = {HealAmount}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(15);
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
                br.Write(HealAmount);
            }
        }

        #endregion

    }
}
