using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdatePlayerLuckFactors (0x86) packet.
    /// </summary>
    public class UpdatePlayerLuckFactors : TerrariaPacket
    {
        public byte PlayerID { get; set; }

        public int LadybugLuckTimeRemaining { get; set; }

        public float TorchLuck { get; set; }

        public byte LuckPotion { get; set; }

        public bool HasGardenGnomeNearby { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayerLuckFactors"/> class.
        /// </summary>
        public UpdatePlayerLuckFactors()
            : base((byte)PacketTypes.UpdatePlayerLuckFactors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayerLuckFactors"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdatePlayerLuckFactors(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.LadybugLuckTimeRemaining = br.ReadInt32();
            this.TorchLuck = br.ReadSingle();
            this.LuckPotion = br.ReadByte();
            this.HasGardenGnomeNearby = br.ReadBoolean();
        }

        public override string ToString()
        {
            return $"[UpdatePlayerLuckFactors: PlayerID = {PlayerID}, LadybugLuckTimeRemaining = {LadybugLuckTimeRemaining}, TorchLuck = {TorchLuck}, LuckPotion = {LuckPotion}, HasGardenGnomeNearby = {HasGardenGnomeNearby}]";
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
                br.Write(LadybugLuckTimeRemaining);
                br.Write(TorchLuck);
                br.Write(LuckPotion);
                br.Write(HasGardenGnomeNearby);
            }
        }

        #endregion

    }
}
