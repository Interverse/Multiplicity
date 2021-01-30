using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ContinueConnecting (0x3) packet.
    /// </summary>
    public class SetUserSlot : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserSlot"/> class.
        /// </summary>
        public SetUserSlot()
            : base((byte)PacketTypes.SetUserSlot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetUserSlot"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SetUserSlot(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[SetUserSlot: PlayerID = {PlayerID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(1);
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
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
