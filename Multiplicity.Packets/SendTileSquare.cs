using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SendTileSquare (0x14) packet.
    /// </summary>
    public class SendTileSquare : TerrariaPacket
    {

        public ushort PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the TileChangeType - Only if != 0|
        /// </summary>
        public byte TileChangeType { get; set; }

        public short Size { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        public byte[] TilePayload { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendTileSquare"/> class.
        /// </summary>
        public SendTileSquare()
            : base((byte)PacketTypes.SendTileSquare)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendTileSquare"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SendTileSquare(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadUInt16();

            int num24 = 32768;
            int num25 = (uint)(this.PlayerID & num24) > 0U ? 1 : 0;

            if (num25 != 0)
                this.TileChangeType = br.ReadByte();

            this.Size = br.ReadInt16();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();

            this.TilePayload = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
        }

        public override string ToString()
        {
            return $"[SendTileSquare: PlayerID = {PlayerID} TileChangeType = {TileChangeType} Size = {Size} TileX = {TileX} TileY = {TileY} TileData: {TilePayload.Length / 1024:0.###} kB]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(9 + TilePayload.Length);
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

                int num24 = 32768;
                int num25 = (uint)(this.PlayerID & num24) > 0U ? 1 : 0;

                if (num25 != 0)
                    br.Write(TileChangeType);
                
                br.Write(Size);
                br.Write(TileX);
                br.Write(TileY);
                br.Write(TilePayload);
            }
        }

        #endregion

    }
}
