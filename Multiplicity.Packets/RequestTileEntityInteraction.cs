using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The RequestTileEntityInteraction (0x7A) packet.
    /// </summary>
    public class RequestTileEntityInteraction : TerrariaPacket
    {

        public int TileEntityID { get; set; }

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTileEntityInteraction"/> class.
        /// </summary>
        public RequestTileEntityInteraction()
            : base((byte)PacketTypes.RequestTileEntityInteraction)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTileEntityInteraction"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public RequestTileEntityInteraction(BinaryReader br)
            : base(br)
        {
            this.TileEntityID = br.ReadInt32();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[RequestTileEntityInteraction: TileEntityID = {TileEntityID}, PlayerID = {PlayerID}]";
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
                br.Write(TileEntityID);
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
