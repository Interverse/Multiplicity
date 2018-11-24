using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ProjectileUpdate (0x1B) packet.
    /// </summary>
    public class ProjectileUpdate : TerrariaPacket
    {

        public short ProjectileID { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public float KnockBack { get; set; }

        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the Owner - Player ID|
        /// </summary>
        public byte Owner { get; set; }

        public short Type { get; set; }

        /// <summary>
        /// Gets or sets the AIFlags - BitFlags: 0 = AI[0] is Present, 1 = AI[1] is Present, 2 = Needs UUID|
        /// </summary>
        public byte AIFlags { get; set; }

        /// <summary>
        /// Gets or sets the AI0 - Requires the AI0 flag to be set in order to be sent down the wire|
        /// </summary>
        public float AI0 { get; set; }

        /// <summary>
        /// Gets or sets the AI1 - Requires the AI1 flag to be set in order to be sent down the wire|
        /// </summary>
        public float AI1 { get; set; }

        /// <summary>
        /// Gets or sets the ProjUUID - Requires the Needs UUID flag to be set in order to be sent down the wire|
        /// </summary>
        public short ProjUUID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileUpdate"/> class.
        /// </summary>
        public ProjectileUpdate()
            : base((byte)PacketTypes.ProjectileUpdate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileUpdate"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ProjectileUpdate(BinaryReader br)
            : base(br)
        {
            this.ProjectileID = br.ReadInt16();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();
            this.VelocityX = br.ReadSingle();
            this.VelocityY = br.ReadSingle();
            this.KnockBack = br.ReadSingle();
            this.Damage = br.ReadInt16();
            this.Owner = br.ReadByte();
            this.Type = br.ReadInt16();
            this.AIFlags = br.ReadByte();

            if (this.AIFlags.ReadBit(0))
                this.AI0 = br.ReadSingle();
            if (this.AIFlags.ReadBit(1))
                this.AI1 = br.ReadSingle();
            if (this.AIFlags.ReadBit(2))
                this.ProjUUID = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[ProjectileUpdate: ProjectileID = {ProjectileID} PositionX = {PositionX} PositionY = {PositionY} VelocityX = {VelocityX} VelocityY = {VelocityY} KnockBack = {KnockBack} Damage = {Damage} Owner = {Owner} Type = {Type} AIFlags = {AIFlags} AI0 = {AI0} AI1 = {AI1} ProjUUID = {ProjUUID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            byte length = 28;
            if (this.AIFlags.ReadBit(0))
                length += 4;
            if (this.AIFlags.ReadBit(1))
                length += 4;
            if (this.AIFlags.ReadBit(2))
                length += 2;
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
                br.Write(ProjectileID);
                br.Write(PositionX);
                br.Write(PositionY);
                br.Write(VelocityX);
                br.Write(VelocityY);
                br.Write(KnockBack);
                br.Write(Damage);
                br.Write(Owner);
                br.Write(Type);
                br.Write(AIFlags);
                if (this.AIFlags.ReadBit(0))
                    br.Write(AI0);
                if (this.AIFlags.ReadBit(1))
                    br.Write(AI1);
                if (this.AIFlags.ReadBit(2))
                    br.Write(ProjUUID);
            }
        }

        #endregion

    }
}
