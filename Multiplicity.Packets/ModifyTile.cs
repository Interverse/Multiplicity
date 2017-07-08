using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ModifyTile (0x11) packet.
    /// </summary>
    public class ModifyTile : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Action - Values: 0 = KillTile, 1 = PlaceTile, 2 = KillWall, 3 = PlaceWall, 4 = KillTileNoItem, 5 = PlaceWire, 6 = KillWire, 7 = PoundTile, 8 = PlaceActuator, 9 = KillActuator, 10 = PlaceWire2, 11 = KillWire2, 12 = PlaceWire3, 13 = KillWire3, 14 = SlopeTile, 15 = FrameTrack, 16 = PlaceWire4, 17 = KillWire4, 18 = PokeLogicGate, 19 = Actuate|
        /// </summary>
        public byte Action { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the Var1 - KillTile (Fail: Bool), PlaceTile (Type: Byte), KillWall (Fail: Bool), PlaceWall (Type: Byte), KillTileNoItem (Fail: Bool), SlopeTile (Slope: Byte)|
        /// </summary>
        public short Var1 { get; set; }

        /// <summary>
        /// Gets or sets the Var2 - Var2: PlaceTile (Style: Byte)|
        /// </summary>
        public byte Var2 { get; set; }

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
            this.Var1 = br.ReadInt16();
            this.Var2 = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[ModifyTile: Action = {Action} TileX = {TileX} TileY = {TileY} Var1 = {Var1} Var2 = {Var2}]";
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
                br.Write(Var1);
                br.Write(Var2);
            }
        }

        #endregion

    }
}
