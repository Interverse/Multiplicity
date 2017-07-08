using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncEmoteBubble (0x5B) packet.
    /// </summary>
    public class SyncEmoteBubble : TerrariaPacket
    {

        public int EmoteID { get; set; }

        public byte AnchorType { get; set; }

        /// <summary>
        /// Gets or sets the MetaData - Only if AnchorType != 255|
        /// </summary>
        public ushort MetaData { get; set; }

        /// <summary>
        /// Gets or sets the Lifetime - Only if AnchorType != 255|
        /// </summary>
        public byte Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the Emote - Only if AnchorType != 255|
        /// </summary>
        public byte Emote { get; set; }

		/// <summary>
		/// Gets or sets the EmoteMetaData - Only sent if AnchorType != 255 and Emote < 0|
		/// </summary>
		public short EmoteMetaData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEmoteBubble"/> class.
        /// </summary>
        public SyncEmoteBubble()
            : base((byte)PacketTypes.SyncEmoteBubble)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEmoteBubble"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncEmoteBubble(BinaryReader br)
            : base(br)
        {
            this.EmoteID = br.ReadInt32();
            this.AnchorType = br.ReadByte();
            if (this.AnchorType != 255)
            {
                this.MetaData = br.ReadUInt16();
                this.Lifetime = br.ReadByte();
                this.Emote = br.ReadByte();
                if (this.Emote < 0)
                    this.EmoteMetaData = br.ReadInt16();
            }

        }

        public override string ToString()
        {
            return $"[SyncEmoteBubble: EmoteID = {EmoteID} AnchorType = {AnchorType} MetaData = {MetaData} Lifetime = {Lifetime} Emote = {Emote} EmoteMetaData = {EmoteMetaData}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            short length = 5;

            if (this.AnchorType != 255)
            {
                length += 4;
                if (this.Emote < 0)
                    length += 2;
            }
            return length;
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
                br.Write(EmoteID);
                br.Write(AnchorType);
                if (this.AnchorType != 255)
                {
                    br.Write(MetaData);
                    br.Write(Lifetime);
                    br.Write(Emote);
                    if (this.Emote < 0)
                        br.Write(EmoteMetaData);
                }
            }
        }

        #endregion

    }
}
