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

        public int TileEntityId { get; set; }

        /// <summary>
        /// If UpdateTileFlag is false, TileEntity is removed
        /// </summary>
        public bool UpdateTileFlag { get; set; }

        public byte TileEntityType { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

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
            this.TileEntityId = br.ReadInt32();

            if (!this.UpdateTileFlag)
            {
                this.TileEntityType = br.ReadByte();
                this.X = br.ReadInt16();
                this.Y = br.ReadInt16();
            }
        }

        public override string ToString()
        {
            return $"[UpdateTileEntity: TileEntityId = {TileEntityId} UpdateTileFlag = {UpdateTileFlag} TileEntityType = {TileEntityType} X = {X} Y = {Y}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            short length = 5;
            if (!UpdateTileFlag)
            {
                length += 5;
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
                br.Write(TileEntityId);
                br.Write(UpdateTileFlag);

                if (!this.UpdateTileFlag)
                {
                    br.Write(TileEntityType);
                    br.Write(X);
                    br.Write(Y);
                }
            }
        }

        #endregion

    }
}
