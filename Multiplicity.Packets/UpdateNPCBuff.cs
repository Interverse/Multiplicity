using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateNPCBuff (0x36) packet.
    /// </summary>
    public class UpdateNPCBuff : TerrariaPacket
    {

        public short NPCID { get; set; }

        public byte BuffID { get; set; }

        public short Time { get; set; }

        public byte BuffID2 { get; set; }

        public short Time2 { get; set; }

        public byte BuffID3 { get; set; }

        public short Time3 { get; set; }

        public byte BuffID4 { get; set; }

        public short Time4 { get; set; }

        public byte BuffID5 { get; set; }

        public short Time5 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNPCBuff"/> class.
        /// </summary>
        public UpdateNPCBuff()
            : base((byte)PacketTypes.UpdateNPCBuff)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNPCBuff"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateNPCBuff(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.BuffID = br.ReadByte();
            this.Time = br.ReadInt16();
            this.BuffID2 = br.ReadByte();
            this.Time2 = br.ReadInt16();
            this.BuffID3 = br.ReadByte();
            this.Time3 = br.ReadInt16();
            this.BuffID4 = br.ReadByte();
            this.Time4 = br.ReadInt16();
            this.BuffID5 = br.ReadByte();
            this.Time5 = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[UpdateNPCBuff: NPCID = {NPCID} BuffID = {BuffID} Time = {Time} BuffID2 = {BuffID2} Time2 = {Time2} BuffID3 = {BuffID3} Time3 = {Time3} BuffID4 = {BuffID4} Time4 = {Time4} BuffID5 = {BuffID5} Time5 = {Time5}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(17);
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
                br.Write(NPCID);
                br.Write(BuffID);
                br.Write(Time);
                br.Write(BuffID2);
                br.Write(Time2);
                br.Write(BuffID3);
                br.Write(Time3);
                br.Write(BuffID4);
                br.Write(Time4);
                br.Write(BuffID5);
                br.Write(Time5);
            }
        }

        #endregion

    }
}
