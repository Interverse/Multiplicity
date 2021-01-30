using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerZone (0x24) packet.
    /// </summary>
    public class PlayerZone : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the Zone1 - See <see cref="Zone1Flags"/> for flag list|
        /// </summary>
        public byte Zone1 { get; set; }

        /// <summary>
        /// Gets or sets the Zone2 - See <see cref="Zone2Flags"/> for flag list|
        /// </summary>
        public byte Zone2 { get; set; }

        /// <summary>
        /// Gets or sets the Zone3 - See <see cref="Zone3Flags"/> for flag list|
        /// </summary>
        public byte Zone3 { get; set; }

        /// <summary>
        /// Gets or sets the Zone4 - See <see cref="Zone4Flags"/> for flag list|
        /// </summary>
        public byte Zone4 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerZone"/> class.
        /// </summary>
        public PlayerZone()
            : base((byte)PacketTypes.PlayerZone)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerZone"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerZone(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Zone1 = br.ReadByte();
            this.Zone2 = br.ReadByte();
            this.Zone3 = br.ReadByte();
            this.Zone4 = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PlayerZone: PlayerID = {PlayerID} Zone1 = {Zone1} Zone2 = {Zone2} Zone3 = {Zone3} Zone4 = {Zone4}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5);
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
                br.Write(Zone1);
                br.Write(Zone2);
                br.Write(Zone3);
                br.Write(Zone4);
            }
        }

        #endregion

    }
}
