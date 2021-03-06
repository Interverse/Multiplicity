using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The Disconnect (0x2) packet.
    /// </summary>
    public class Disconnect : TerrariaPacket
    {

        public NetworkText Reason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnect"/> class.
        /// </summary>
        public Disconnect()
            : base((byte)PacketTypes.Disconnect)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnect"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public Disconnect(BinaryReader br)
            : base(br)
        {
            this.Reason = br.ReadNetworkText();
        }

        public override string ToString()
        {
            return $"[Disconnect: Reason = {Reason.Text}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(0 + Reason.GetLength());
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
                br.Write(Reason);
            }
        }

        #endregion

    }
}
