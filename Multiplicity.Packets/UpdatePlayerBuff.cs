using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdatePlayerBuff (0x32) packet.
    /// </summary>
    public class UpdatePlayerBuff : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte[] BuffType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayerBuff"/> class.
        /// </summary>
        public UpdatePlayerBuff()
            : base((byte)PacketTypes.UpdatePlayerBuff)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayerBuff"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdatePlayerBuff(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.BuffType = br.ReadBytes(22);
        }

        public override string ToString()
        {
            return $"[UpdatePlayerBuff: PlayerID = {PlayerID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(23);
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
                br.Write(PlayerID);
                br.Write(BuffType);
            }
        }

        #endregion

    }
}
