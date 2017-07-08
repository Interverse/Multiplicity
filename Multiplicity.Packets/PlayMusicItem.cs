using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayMusicItem (0x3A) packet.
    /// </summary>
    public class PlayMusicItem : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public float Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayMusicItem"/> class.
        /// </summary>
        public PlayMusicItem()
            : base((byte)PacketTypes.PlayMusicItem)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayMusicItem"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayMusicItem(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Note = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[PlayMusicItem: PlayerID = {PlayerID} Note = {Note}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5);
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
                br.Write(Note);
            }
        }

        #endregion

    }
}
