using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The RequestNPCBuffRemoval (0x89) packet.
    /// </summary>
    public class RequestNPCBuffRemoval : TerrariaPacket
    {
        public short NpcID { get; set; }

        public ushort BuffID { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RequestNPCBuffRemoval"/> class.
        /// </summary>
        public RequestNPCBuffRemoval()
            : base((byte)PacketTypes.RequestNPCBuffRemoval)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestNPCBuffRemoval"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public RequestNPCBuffRemoval(BinaryReader br)
            : base(br)
        {
            this.NpcID = br.ReadInt16();
            this.BuffID = br.ReadUInt16();
        }

        public override string ToString()
        {
            return $"[RequestNPCBuffRemoval: NpcID = {NpcID}, BuffID = {BuffID}]";
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
                br.Write(NpcID);
                br.Write(BuffID);
            }
        }

        #endregion

    }
}
