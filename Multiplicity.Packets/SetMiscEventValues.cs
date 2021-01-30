using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SetMiscEventValues (0x8C) packet.
    /// </summary>
    public class SetMiscEventValues : TerrariaPacket
    {
        /// <summary>
        /// Value is never used but set as 0. 
        /// </summary>
        public byte Unused { get; set; }

        /// <summary>
        /// Clamped. Min 0, Max 28800.
        /// </summary>
        public int CreditsRollRemainingTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetMiscEventValues"/> class.
        /// </summary>
        public SetMiscEventValues()
            : base((byte)PacketTypes.SetMiscEventValues)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetMiscEventValues"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SetMiscEventValues(BinaryReader br)
            : base(br)
        {
            this.Unused = br.ReadByte();
            this.CreditsRollRemainingTime = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[SetMiscEventValues: Unused = {Unused}, CreditsRollRemainingTime = {CreditsRollRemainingTime}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5);
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
                br.Write(Unused);
                br.Write(CreditsRollRemainingTime);
            }
        }

        #endregion

    }
}
