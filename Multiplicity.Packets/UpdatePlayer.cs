using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdatePlayer (0xD) packet.
    /// </summary>
    public class UpdatePlayer : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the Control - See <see cref="ControlFlags"/>|
        /// </summary>
        public byte Control { get; set; }

        /// <summary>
        /// Gets or sets the Pulley - See <see cref="PulleyFlags"/>|
        /// </summary>
        public byte Pulley { get; set; }

        /// <summary>
        /// Gets or sets the Misc - See <see cref="MiscFlags"/>|
        /// </summary>
        public byte Misc { get; set; }

        /// <summary>
        /// Gets or sets the SleepingInfo - See <see cref="SleepingInfoFlags"/>|
        /// </summary>
        public byte SleepingInfo { get; set; }

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
        /// Original Position X for Potion of Return, only sent if UsedPotionofReturn flag is true|
        /// </summary>
        public float OriginalPositionX { get; set; }

        /// <summary>
        /// Original Position Y for Potion of Return, only sent if UsedPotionofReturn flag is true|
        /// </summary>
        public float OriginalPositionY { get; set; }

        /// <summary>
        /// Home Position X for Potion of Return, only sent if UsedPotionofReturn flag is true|
        /// </summary>
        public float HomePositionX { get; set; }

        /// <summary>
        /// Home Position Y for Potion of Return, only sent if UsedPotionofReturn flag is true|
        /// </summary>
        public float HomePositionY { get; set; }

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
            this.Misc = br.ReadByte();
            this.SleepingInfo = br.ReadByte();
            this.SelectedItem = br.ReadByte();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();

            if (this.Pulley.ReadFlag(PulleyFlags.UpdateVelocity))
            {
                this.VelocityX = br.ReadSingle();
                this.VelocityY = br.ReadSingle();
            }

            if (this.Misc.ReadFlag(MiscFlags.UsedPotionofReturn))
            {
                this.OriginalPositionX = br.ReadSingle();
                this.OriginalPositionY = br.ReadSingle();
                this.HomePositionX = br.ReadSingle();
                this.HomePositionY = br.ReadSingle();
            }
        }

        public override string ToString()
        {
            return $"[UpdatePlayer: PlayerID = {PlayerID} Control = {Control} Pulley = {Pulley} Misc = {Misc} SleepingInfo = {SleepingInfo} SelectedItem = {SelectedItem} PositionX = {PositionX} PositionY = {PositionY} VelocityX = {VelocityX} VelocityY = {VelocityY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            byte length = 14;
            if (Pulley.ReadFlag(PulleyFlags.UpdateVelocity))
                length += 8;
            if (Misc.ReadFlag(MiscFlags.UsedPotionofReturn))
                length += 16;
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

                if (this.Pulley.ReadFlag(PulleyFlags.UpdateVelocity))
                {
                    br.Write(VelocityX);
                    br.Write(VelocityY);
                }

                if (this.Misc.ReadFlag(MiscFlags.UsedPotionofReturn))
                {
                    br.Write(OriginalPositionX);
                    br.Write(OriginalPositionY);
                    br.Write(HomePositionY);
                    br.Write(HomePositionY);
                }
            }
        }

        #endregion

    }
}
