using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdatePlayer (0xD) packet.
    /// </summary>
    public class UpdatePlayer : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the Control - BitFlags: ControlUp = 1, ControlDown = 2, ControlLeft = 4, ControlRight = 8, ControlJump = 16, ControlUseItem = 32, Direction = 64|
        /// </summary>
        public byte Control { get; set; }

        /// <summary>
        /// Gets or sets the Pulley - BitFlags: 0 = None, 1 = Direction, 2 = Direction, 4 = Update Velocity, 8 = Vortex Stealth Active, 16 = Gravity Direction, 32 = Shield Raised|
        /// </summary>
        public byte Pulley { get; set; }

        public byte SelectedItem { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        /// <summary>
        /// Gets or sets the VelocityX - Not sent if Update Velocity is not set|
        /// </summary>
        public float VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the VelocityY - Not sent if Update Velocity is not set|
        /// </summary>
        public float VelocityY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayer"/> class.
        /// </summary>
        public UpdatePlayer()
            : base((byte)PacketTypes.UpdatePlayer)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePlayer"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdatePlayer(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Control = br.ReadByte();
            this.Pulley = br.ReadByte();
            this.SelectedItem = br.ReadByte();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();

            if (this.Pulley.ReadBit(2))
            {
                this.VelocityX = br.ReadSingle();
                this.VelocityY = br.ReadSingle();
            }
        }

        public override string ToString()
        {
            return $"[UpdatePlayer: PlayerID = {PlayerID} Control = {Control} Pulley = {Pulley} SelectedItem = {SelectedItem} PositionX = {PositionX} PositionY = {PositionY} VelocityX = {VelocityX} VelocityY = {VelocityY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            byte length = 12;
            if (Pulley.ReadBit(2))
                length += 8;
            return (short)(length);
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
                br.Write(Control);
                br.Write(Pulley);
                br.Write(SelectedItem);
                br.Write(PositionX);
                br.Write(PositionY);

                if (this.Pulley.ReadBit(2))
                {
                    br.Write(VelocityX);
                    br.Write(VelocityY);
                }
            }
        }

        #endregion

    }
}
