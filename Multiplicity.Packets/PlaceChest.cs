using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlaceChest (0x22) packet.
    /// </summary>
    public class PlaceChest : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the ChestID - BitFlags:0 = Place Chest, 1 = Kill Chest, 2 = Place Dresser, 3 = Kill Dresser. 4 = Place Containers2, 5 = Kill Containers2|
        /// </summary>
        public byte ChestID { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the Style - FrameX(Chest type)|
        /// </summary>
        public short Style { get; set; }

        /// <summary>
        /// Gets or sets the ChestIDtodestroy - ID if client is receiving packet, else 0|
        /// </summary>
        public short ChestIDtodestroy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceChest"/> class.
        /// </summary>
        public PlaceChest()
            : base((byte)PacketTypes.PlaceChest)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceChest"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlaceChest(BinaryReader br)
            : base(br)
        {
            this.ChestID = br.ReadByte();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
            this.Style = br.ReadInt16();
            this.ChestIDtodestroy = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[PlaceChest: ChestID = {ChestID} TileX = {TileX} TileY = {TileY} Style = {Style} ChestIDtodestroy = {ChestIDtodestroy}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(9);
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
                br.Write(ChestID);
                br.Write(TileX);
                br.Write(TileY);
                br.Write(Style);
                br.Write(ChestIDtodestroy);
            }
        }

        #endregion

    }
}
