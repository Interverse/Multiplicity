using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SetChestName (0x21) packet.
    /// </summary>
    public class SetChestName : TerrariaPacket
    {

        public short ChestID { get; set; }

        public short ChestX { get; set; }

        public short ChestY { get; set; }

        public byte NameLength { get; set; }

        /// <summary>
        /// Gets or sets the ChestName - Only if length > 0 && <= 20|
        /// </summary>
        public string ChestName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetChestName"/> class.
        /// </summary>
        public SetChestName()
            : base((byte)PacketTypes.SetChestName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetChestName"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SetChestName(BinaryReader br)
            : base(br)
        {
            this.ChestID = br.ReadInt16();
            this.ChestX = br.ReadInt16();
            this.ChestY = br.ReadInt16();
            this.NameLength = br.ReadByte();
            this.ChestName = String.Empty;

            if (this.NameLength >= 0 && this.NameLength <= 20)
                this.ChestName = br.ReadString();
            else
                this.NameLength = 0;
        }

        public override string ToString()
        {
            return $"[SetChestName: ChestID = {ChestID} ChestX = {ChestX} ChestY = {ChestY} NameLength = {NameLength} ChestName = {ChestName}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8 + ChestName?.Length);
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
                br.Write(ChestX);
                br.Write(ChestY);
                NameLength = (byte)ChestName?.Length;
                br.Write(NameLength);
                if (ChestName != null)
                    br.Write(ChestName);
            }
        }

        #endregion

    }
}
