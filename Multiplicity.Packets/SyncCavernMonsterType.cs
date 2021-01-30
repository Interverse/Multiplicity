using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncCavernMonsterType (0x88) packet.
    /// </summary>
    public class SyncCavernMonsterType : TerrariaPacket
    {
        /// <summary>
        /// NPC.cavernMonsterType[0,0]
        /// </summary>
        public ushort NetID1 { get; set; }

        /// <summary>
        /// NPC.cavernMonsterType[0,1]
        /// </summary>
        public ushort NetID2 { get; set; }

        /// <summary>
        /// NPC.cavernMonsterType[0,2]
        /// </summary>
        public ushort NetID3 { get; set; }

        /// <summary>
        /// NPC.cavernMonsterType[1,0]
        /// </summary>
        public ushort NetID4 { get; set; }

        /// <summary>
        /// NPC.cavernMonsterType[1,1]
        /// </summary>
        public ushort NetID5 { get; set; }

        /// <summary>
        /// NPC.cavernMonsterType[1,2]
        /// </summary>
        public ushort NetID6 { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCavernMonsterType"/> class.
        /// </summary>
        public SyncCavernMonsterType()
            : base((byte)PacketTypes.SyncCavernMonsterType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCavernMonsterType"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncCavernMonsterType(BinaryReader br)
            : base(br)
        {
            this.NetID1 = br.ReadUInt16();
            this.NetID2 = br.ReadUInt16();
            this.NetID3 = br.ReadUInt16();
            this.NetID4 = br.ReadUInt16();
            this.NetID5 = br.ReadUInt16();
            this.NetID6 = br.ReadUInt16();
        }

        public override string ToString()
        {
            return $"[SyncCavernMonsterType: NetID1 = {NetID1}, NetID2 = {NetID2}, NetID3 = {NetID3}, NetID4 = {NetID4}, NetID5 = {NetID5}, NetID6 = {NetID6}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(12);
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
                br.Write(NetID1);
                br.Write(NetID2);
                br.Write(NetID3);
                br.Write(NetID4);
                br.Write(NetID5);
                br.Write(NetID6);
            }
        }

        #endregion

    }
}
