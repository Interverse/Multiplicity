using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using System.Drawing;
using Multiplicity.Packets.Models;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ChatMessagev2 (0x6B) packet.
    /// </summary>
    public class ChatMessagev2 : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the MessageColor - Client cannot change colors|
        /// </summary>
        public Color MessageColor { get; set; }

        /// <summary>
        /// Gets or sets the Message - |-|
        /// </summary>
        public NetworkText Message { get; set; }

        public short MessageLength { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessagev2"/> class.
        /// </summary>
        public ChatMessagev2()
            : base((byte)PacketTypes.ChatMessagev2)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessagev2"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ChatMessagev2(BinaryReader br)
            : base(br)
        {
            this.MessageColor = br.ReadColor();
            this.Message = br.ReadNetworkText();
            this.MessageLength = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[ChatMessagev2: MessageColor = {MessageColor} Message = {Message.Text} MessageLength = {MessageLength}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5 + Message.GetLength());
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
                br.Write(MessageColor);
                br.Write(Message);
                br.Write(MessageLength);
            }
        }

        #endregion

    }
}
