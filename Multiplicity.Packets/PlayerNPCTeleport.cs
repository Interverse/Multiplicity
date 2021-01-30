using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerNPCTeleport (0x41) packet.
    /// </summary>
    public class PlayerNPCTeleport : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Flags - See <see cref="PlayerNPCTeleportFlags"/> for flag list|
        /// </summary>
        public byte Flags { get; set; }

        public short TargetID { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public byte Style { get; set; }

        /// <summary>
        /// Only sent if HasExtraInfo flag is true
        /// </summary>
        public int ExtraInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNPCTeleport"/> class.
        /// </summary>
        public PlayerNPCTeleport()
            : base((byte)PacketTypes.PlayerNPCTeleport)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNPCTeleport"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerNPCTeleport(BinaryReader br)
            : base(br)
        {
            this.Flags = br.ReadByte();
            this.TargetID = br.ReadInt16();
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
            this.Style = br.ReadByte();
            if (this.Flags.ReadFlag(PlayerNPCTeleportFlags.HasExtraInfo))
                this.ExtraInfo = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[PlayerNPCTeleport: Flags = {Flags} TargetID = {TargetID} X = {X} Y = {Y} Style = {Style} ExtraInfo = {ExtraInfo}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            short length = 12;
            if (this.Flags.ReadFlag(PlayerNPCTeleportFlags.HasExtraInfo))
                length += 4;
            return length;
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
                br.Write(Flags);
                br.Write(TargetID);
                br.Write(X);
                br.Write(Y);
                br.Write(Style);
                br.Write(ExtraInfo);
            }
        }

        #endregion

    }
}
