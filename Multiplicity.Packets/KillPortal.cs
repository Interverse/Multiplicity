using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The KillPortal (0x5F) packet.
    /// </summary>
    public class KillPortal : TerrariaPacket
    {

        public ushort ProjectileOwner { get; set; }
        public byte ProjectileAI { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KillPortal"/> class.
        /// </summary>
        public KillPortal()
            : base((byte)PacketTypes.KillPortal)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KillPortal"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public KillPortal(BinaryReader br)
            : base(br)
        {
            this.ProjectileOwner = br.ReadUInt16();
            this.ProjectileAI = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[KillPortal: ProjectileOwner = {ProjectileOwner} ProjectileAI = {ProjectileAI}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(2);
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
                br.Write(ProjectileOwner);
                br.Write(ProjectileAI);
            }
        }

        #endregion

    }
}
