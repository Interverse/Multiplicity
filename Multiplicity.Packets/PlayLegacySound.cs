using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayLegacySound (0x84) packet.
    /// </summary>
    public class PlayLegacySound : TerrariaPacket
    {
        public float X { get; set; }

        public float Y { get; set; }

        public ushort SoundID { get; set; }

        /// <summary>
        /// BitFlags: 1 = Style, 2 = Volume Scale, 3 = Pitch Offset
        /// </summary>

        public byte SoundFlags { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PlayLegacySound"/> class.
        /// </summary>
        public PlayLegacySound()
            : base((byte)PacketTypes.PlayLegacySound)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayLegacySound"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayLegacySound(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
            this.SoundID = br.ReadUInt16();
            this.SoundFlags = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PlayLegacySound: X = {X}, Y = {Y}, SoundID = {SoundID}, SoundFlags = {SoundFlags}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(11);
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
                br.Write(SoundID);
                br.Write(SoundFlags);
            }
        }

        #endregion

    }
}
