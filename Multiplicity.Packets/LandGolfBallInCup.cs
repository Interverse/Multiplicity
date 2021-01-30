using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The LandGolfBallInCup (0x80) packet.
    /// </summary>
    public class LandGolfBallInCup : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public ushort NumberofHits { get; set; }

        public ushort ProjID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandGolfBallInCup"/> class.
        /// </summary>
        public LandGolfBallInCup()
            : base((byte)PacketTypes.LandGolfBallInCup)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandGolfBallInCup"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public LandGolfBallInCup(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.X = br.ReadUInt16();
            this.Y = br.ReadUInt16();
            this.NumberofHits = br.ReadUInt16();
            this.ProjID = br.ReadUInt16();
        }

        public override string ToString()
        {
            return $"[LandGolfBallInCup: PlayerID = {PlayerID}, X = {X}, Y = {Y}, NumberofHits = {NumberofHits}, ProjID = {ProjID}]";
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
                br.Write(PlayerID);
                br.Write(X);
                br.Write(Y);
                br.Write(NumberofHits);
                br.Write(ProjID);
            }
        }

        #endregion

    }
}
