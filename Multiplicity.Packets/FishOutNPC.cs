using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The FishOutNPC (0x82) packet.
    /// </summary>
    public class FishOutNPC : TerrariaPacket
    {

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public short NpcID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FishOutNPC"/> class.
        /// </summary>
        public FishOutNPC()
            : base((byte)PacketTypes.FishOutNPC)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FishOutNPC"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public FishOutNPC(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"[FishOutNPC: X = {X}, Y = {Y}, NpcID = {NpcID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6);
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
                br.Write(NpcID);
            }
        }

        #endregion

    }
}
