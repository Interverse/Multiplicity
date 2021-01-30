using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ModifyTile (0x11) packet.
    /// </summary>
    public class ModifyTile : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Action - See <see cref="ModifyTileActionFlags"/> for byte list|
        /// </summary>
        public byte Action { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the Flags1 - KillTile (Fail: Bool), PlaceTile (Type: Byte), KillWall (Fail: Bool), PlaceWall (Type: Byte), KillTileNoItem (Fail: Bool), SlopeTile (Slope: Byte), ReplaceTile (Type: Int16), ReplaceWall (Type: Int16)|
        /// </summary>
        public short Flags1 { get; set; }

        /// <summary>
        /// Gets or sets the Flags2 - PlaceTile (Style: Byte), ReplaceTile (Style: Byte)|
        /// </summary>
        public byte Flags2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyTile"/> class.
        /// </summary>
        public ModifyTile()
            : base((byte)PacketTypes.ModifyTile)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyTile"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ModifyTile(BinaryReader br)
            : base(br)
        {
            this.Action = br.ReadByte();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
            this.Flags1 = br.ReadInt16();
            this.Flags2 = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[ModifyTile: Action = {Action} TileX = {TileX} TileY = {TileY} Flags1 = {Flags1} Flags2 = {Flags2}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8);
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
                br.Write(Action);
                br.Write(TileX);
                br.Write(TileY);
                br.Write(Flags1);
                br.Write(Flags2);
            }
        }

        #endregion

    }
}
