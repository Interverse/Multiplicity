using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncTilePicking (0x7D) packet.
    /// </summary>
    public class SyncTilePicking : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public byte PickDamage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTilePicking"/> class.
        /// </summary>
        public SyncTilePicking()
            : base((byte)PacketTypes.SyncTilePicking)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTilePicking"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncTilePicking(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.PickDamage = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[SyncTilePicking: PlayerID = {PlayerID}, X = {X}, Y = {Y}, PickDamage = {PickDamage}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6);
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
                br.Write(X);
                br.Write(Y);
                br.Write(PickDamage);
            }
        }

        #endregion

    }
}
