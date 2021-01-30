using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.Models;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The Status (0x9) packet.
    /// </summary>
    public class Status : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the StatusMax - Status only increases|
        /// </summary>
        public int StatusMax { get; set; }

        public NetworkText StatusText { get; set; }

        /// <summary>
        /// See <see cref="StatusTextFlags"/> for byte list
        /// </summary>
        public byte StatusTextFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
            : base((byte)PacketTypes.Status)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public Status(BinaryReader br)
            : base(br)
        {
            this.StatusMax = br.ReadInt32();
            this.StatusText = br.ReadNetworkText();
            this.StatusTextFlag = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[Status: StatusMax = {StatusMax} StatusText = {StatusText.Text} StatusTextFlags = {StatusText}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5 + StatusText.GetLength());
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
                br.Write(StatusMax);
                br.Write(StatusText);
                br.Write(StatusTextFlag);
            }
        }

        #endregion

    }
}
