using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The WeaponsRackTryPlacing (0x7C) packet.
    /// </summary>
    public class TEHatRackItemSync : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public int TileEntityID { get; set; }

        public byte ItemIndex { get; set; }

        public ushort ItemID { get; set; }

        public ushort Stack { get; set; }

        public byte Prefix { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TEHatRackItemSync"/> class.
        /// </summary>
        public TEHatRackItemSync()
            : base((byte)PacketTypes.TEHatRackItemSync)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TEHatRackItemSync"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public TEHatRackItemSync(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.TileEntityID = br.ReadInt32();
            this.ItemIndex = br.ReadByte();
            this.ItemID = br.ReadUInt16();
            this.Stack = br.ReadUInt16();
            this.Prefix = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[TEHatRackItemSync: PlayerID = {PlayerID}, TileEntityID = {TileEntityID}, ItemIndex = {ItemIndex}, ItemID = {ItemID}, Stack = {Stack}, Prefix = {Prefix}]";
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
                br.Write(PlayerID);
                br.Write(TileEntityID);
                br.Write(ItemIndex);
                br.Write(ItemID);
                br.Write(Stack);
                br.Write(Prefix);
            }
        }

        #endregion

    }
}
