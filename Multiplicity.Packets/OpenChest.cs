using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The OpenChest (0x1F) packet.
    /// </summary>
    public class OpenChest : TerrariaPacket
    {

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenChest"/> class.
        /// </summary>
        public OpenChest()
            : base((byte)PacketTypes.OpenChest)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenChest"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public OpenChest(BinaryReader br)
            : base(br)
        {
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[OpenChest: TileX = {TileX} TileY = {TileY}]";
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
                br.Write(TileX);
                br.Write(TileY);
            }
        }

        #endregion

    }
}
