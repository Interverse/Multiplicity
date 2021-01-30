using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The RequestEssentialTiles (0x8) packet.
    /// </summary>
    public class RequestEssentialTiles : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the X - If -1 Send spawn area tile sections|
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y - If -1 Send spawn area tile sections|
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEssentialTiles"/> class.
        /// </summary>
        public RequestEssentialTiles()
            : base((byte)PacketTypes.RequestEssentialTiles)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestEssentialTiles"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public RequestEssentialTiles(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadInt32();
            this.Y = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[RequestEssentialTiles: X = {X} Y = {Y}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8);
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
            }
        }

        #endregion

    }
}
