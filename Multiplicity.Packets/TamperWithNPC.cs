using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The TamperWithNPC (0x83) packet.
    /// </summary>
    public class TamperWithNPC : TerrariaPacket
    {
        public ushort NpcID { get; set; }

        public ushort SetNPCImmunity { get; set; }

        /// <summary>
        /// Only sent if SetNPCImmunity flag is true
        /// </summary>
        public int ImmunityTime { get; set; }

        /// <summary>
        /// Set to -1 for immunity from all players
        /// </summary>
        public short ImmunityPlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TamperWithNPC"/> class.
        /// </summary>
        public TamperWithNPC()
            : base((byte)PacketTypes.TamperWithNPC)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TamperWithNPC"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public TamperWithNPC(BinaryReader br)
            : base(br)
        {
            this.NpcID = br.ReadUInt16();
            this.SetNPCImmunity = br.ReadUInt16();
            this.ImmunityTime = br.ReadInt32();
            this.ImmunityPlayerID = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[TamperWithNPC: NpcID = {NpcID}, SetNPCImmunity = {SetNPCImmunity}, ImmunityTime = {ImmunityTime}, ImmunityPlayerID = {ImmunityPlayerID}]";
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
                br.Write(NpcID);
                br.Write(SetNPCImmunity);
                br.Write(ImmunityTime);
                br.Write(ImmunityPlayerID);
            }
        }

        #endregion

    }
}
