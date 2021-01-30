using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncRevengeMarker (0x7E) packet.
    /// </summary>
    public class SyncRevengeMarker : TerrariaPacket
    {

        public int UniqueID { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public int NpcID { get; set; }

        public float NpcHPPercent { get; set; }

        public int NpcType { get; set; }

        public int NpcAI { get; set; }

        public int CoinValue { get; set; }

        public float BaseValue { get; set; }

        public bool SpawnedFromStatue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncRevengeMarker"/> class.
        /// </summary>
        public SyncRevengeMarker()
            : base((byte)PacketTypes.SyncRevengeMarker)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncRevengeMarker"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncRevengeMarker(BinaryReader br)
            : base(br)
        {
            this.UniqueID = br.ReadInt32();
            this.X = br.ReadInt32();
            this.Y = br.ReadSingle();
            this.NpcID = br.ReadInt32();
            this.NpcHPPercent = br.ReadSingle();
            this.NpcType = br.ReadInt32();
            this.NpcAI = br.ReadInt32();
            this.CoinValue = br.ReadInt32();
            this.BaseValue = br.ReadSingle();
            this.SpawnedFromStatue = br.ReadBoolean();
        }

        public override string ToString()
        {
            return $"[SyncRevengeMarker: UniqueID = {UniqueID}, X = {X}, Y = {Y}, NpcID = {NpcID}, NpcHPPercent = {NpcHPPercent}, NpcType = {NpcType}, NpcAI = {NpcAI}, CoinValue = {CoinValue}, BaseValue = {BaseValue}, SpawnedFromStatue = {SpawnedFromStatue}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(37);
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
                br.Write(UniqueID);
                br.Write(X);
                br.Write(Y);
                br.Write(NpcID);
                br.Write(NpcHPPercent);
                br.Write(NpcType);
                br.Write(NpcAI);
                br.Write(CoinValue);
                br.Write(BaseValue);
                br.Write(SpawnedFromStatue);
            }
        }

        #endregion

    }
}
