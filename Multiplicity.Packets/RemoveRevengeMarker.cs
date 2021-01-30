using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The RemoveRevengeMarker (0x7F) packet.
    /// </summary>
    public class RemoveRevengeMarker : TerrariaPacket
    {

        public int UniqueID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRevengeMarker"/> class.
        /// </summary>
        public RemoveRevengeMarker()
            : base((byte)PacketTypes.RemoveRevengeMarker)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRevengeMarker"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public RemoveRevengeMarker(BinaryReader br)
            : base(br)
        {
            this.UniqueID = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[RemoveRevengeMarker: UniqueID = {UniqueID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(4);
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
                br.Write(UniqueID);
            }
        }

        #endregion

    }
}
