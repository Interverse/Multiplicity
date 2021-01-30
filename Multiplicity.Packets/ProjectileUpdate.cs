using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

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

        /// <summary>
        /// Gets or sets the Owner - Player ID|
        /// </summary>
        public byte Owner { get; set; }

        public short Type { get; set; }

        /// <summary>
        /// Gets or sets the ProjFlags - Se <see cref="ProjectileUpdateFlags"/> for flag list|
        /// </summary>
        public byte ProjFlags { get; set; }

        /// <summary>
        /// Gets or sets the AI0 - Requires the AI0 flag to be set in order to be sent down the wire|
        /// </summary>
        public float AI0 { get; set; }

        /// <summary>
        /// Gets or sets the AI1 - Requires the AI1 flag to be set in order to be sent down the wire|
        /// </summary>
        public float AI1 { get; set; }

        public short Damage { get; set; }

        public float Knockback { get; set; }

        public float OriginalDamage { get; set; }

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
            this.Owner = br.ReadByte();
            this.Type = br.ReadInt16();
            this.ProjFlags = br.ReadByte();

            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI0))
                this.AI0 = br.ReadSingle();
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI1))
                this.AI1 = br.ReadSingle();
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Damage))
                this.Damage = br.ReadInt16();
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Knockback))
                this.Knockback = br.ReadSingle();
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.OriginalDamage))
                this.OriginalDamage = br.ReadInt16();
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.ProjUUID))
                this.ProjUUID = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[ProjectileUpdate: ProjectileID = {ProjectileID} PositionX = {PositionX} PositionY = {PositionY} VelocityX = {VelocityX} VelocityY = {VelocityY} KnockBack = {Knockback} Damage = {Damage} Owner = {Owner} Type = {Type} AIFlags = {ProjFlags} AI0 = {AI0} AI1 = {AI1} ProjUUID = {ProjUUID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            byte length = 22;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI0))
                length += 4;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI1))
                length += 4;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Damage))
                length += 2;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Knockback))
                length += 4;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.OriginalDamage))
                length += 2;
            if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.ProjUUID))
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
                br.Write(Owner);
                br.Write(Type);
                br.Write(ProjFlags);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI0))
                    br.Write(AI0);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.AI1))
                    br.Write(AI1);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Damage))
                    br.Write(Damage);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.Knockback))
                    br.Write(Knockback);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.OriginalDamage))
                    br.Write(OriginalDamage);
                if (this.ProjFlags.ReadFlag(ProjectileUpdateFlags.ProjUUID))
                    br.Write(ProjUUID);
            }
        }

        #endregion

    }
}
