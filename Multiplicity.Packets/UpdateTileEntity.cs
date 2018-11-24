using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateTileEntity (0x56) packet.
    /// </summary>
    public class UpdateTileEntity : TerrariaPacket
    {

        public int Key { get; set; }

        public bool IsRemove { get; set; }

        /// <summary>
        /// Gets or sets the TileEntityType - If Remove? == false|
        /// </summary>
        public byte TileEntityType { get; set; }

        /// <summary>
        /// Gets or sets the ID - If Remove? == false|
        /// </summary>
        public int TileID { get; set; }

        /// <summary>
        /// Gets or sets the X - If Remove? == false|
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the Y - If Remove? == false|
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the NPC - If Remove? == false && Type = 0|
        /// </summary>
        public short NPC { get; set; }

        /// <summary>
        /// Gets or sets the ItemType - If Remove? == false|
        /// </summary>
        public short ItemType { get; set; }

        /// <summary>
        /// Gets or sets the Prefix - If Remove? == false|
        /// </summary>
        public byte Prefix { get; set; }

        /// <summary>
        /// Gets or sets the Stack - If Remove? == false|
        /// </summary>
        public short Stack { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTileEntity"/> class.
        /// </summary>
        public UpdateTileEntity()
            : base((byte)PacketTypes.UpdateTileEntity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTileEntity"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateTileEntity(BinaryReader br)
            : base(br)
        {
            this.Key = br.ReadInt32();
            this.IsRemove = br.ReadBoolean();

            if (!this.IsRemove)
            {
                this.TileEntityType = br.ReadByte();
                this.TileID = br.ReadInt32();
                this.X = br.ReadInt16();
                this.Y = br.ReadInt16();
                if (this.TileEntityType == 0)
                    this.NPC = br.ReadInt16();
                this.ItemType = br.ReadInt16();
                this.Prefix = br.ReadByte();
                this.Stack = br.ReadInt16();
            }
        }

        public override string ToString()
        {
            return $"[UpdateTileEntity: Key = {Key} IsRemove = {IsRemove} TileEntityType = {TileEntityType} TileID = {TileID} X = {X} Y = {Y} NPC = {NPC} ItemType = {ItemType} Prefix = {Prefix} Stack = {Stack}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            short length = 5;
            if (!IsRemove)
            {
                length += 14;
                if (TileEntityType == 0)
                    length += 2;
            }
            return (short)(length);
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
                br.Write(Key);
                br.Write(IsRemove);

                if (!this.IsRemove)
                {
                    br.Write(TileEntityType);
                    br.Write(TileID);
                    br.Write(X);
                    br.Write(Y);
                    if (this.TileEntityType == 0)
                        br.Write(NPC);
                    br.Write(ItemType);
                    br.Write(Prefix);
                    br.Write(Stack);
                }
            }
        }

        #endregion

    }
}
