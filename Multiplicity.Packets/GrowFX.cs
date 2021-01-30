using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The GrowFX (0x70) packet.
    /// </summary>
    public class GrowFX : TerrariaPacket
    {
        /// <summary>
        /// 1 = Tree Growth Effects, 2 = Fairy Effects
        /// </summary>
        public byte EffectFlags { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        /// <summary>
        /// if EffectFlag is TreeGrowth, data is Height; if EffectFlag is Fairy Effects, data is effect Type
        /// </summary>
        public byte Height { get; set; }

        /// <summary>
        /// Always 0 unless it is TreeGrowth
        /// </summary>
        public short TreeGore { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrowFX"/> class.
        /// </summary>
        public GrowFX()
            : base((byte)PacketTypes.GrowFX)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrowFX"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public GrowFX(BinaryReader br)
            : base(br)
        {
            this.EffectFlags = br.ReadByte();
            this.X = br.ReadInt32();
            this.Y = br.ReadInt32();
            this.Height = br.ReadByte();
            this.TreeGore = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[GrowFX: EffectFlags = {EffectFlags} X = {X} Y = {Y} Height = {Height} TreeGore = {TreeGore}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(12);
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
                br.Write(EffectFlags);
                br.Write(X);
                br.Write(Y);
                br.Write(Height);
                br.Write(TreeGore);
            }
        }

        #endregion

    }
}
