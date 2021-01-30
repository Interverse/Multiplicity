//
//  AlterItemDrop.cs
//
//  Author:
//       Josh Harris <celant@celantinteractive.com>
//
//  Copyright (c) 2016 Celant

using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    public class AlterItemDrop : TerrariaPacket
    {
        public short ItemIndex { get; set; }

        /// <summary>
        /// Sets or Gets Flags1 - See <see cref="ItemDropFlags1"/> for flag list
        /// </summary>
        public byte Flags1 { get; set; }

        public uint PackedColorValue { get; set; }

        public ushort Damage { get; set; }

        public float Knockback { get; set; }

        public ushort UseAnimation { get; set; }

        public ushort UseTime { get; set; }

        public short Shoot { get; set; }

        public float ShootSpeed { get; set; }

        /// <summary>
        /// Sets or Gets Flags2 - See <see cref="ItemDropFlags2"/> for flag list
        /// </summary>
        public byte Flags2 { get; set; }

        public short Width { get; set; }

        public short Height { get; set; }

        public float Scale { get; set; }

        public short Ammo { get; set; }

        public short UseAmmo { get; set; }

        public bool NotAmmo { get; set; }

        public AlterItemDrop()
            : base((byte)PacketTypes.AlterItemDrop)
        {
        }

        public AlterItemDrop(BinaryReader br)
            : base(br)
        {
            this.ItemIndex = br.ReadInt16();
            this.Flags1 = br.ReadByte();
            if (this.Flags1.ReadFlag(ItemDropFlags1.Color))
                this.PackedColorValue = br.ReadUInt32();
            if (this.Flags1.ReadFlag(ItemDropFlags1.Damage))
                this.Damage = br.ReadUInt16();
            if (this.Flags1.ReadFlag(ItemDropFlags1.Knockback))
                this.Knockback = br.ReadSingle();
            if (this.Flags1.ReadFlag(ItemDropFlags1.UseAnimation)) 
                this.UseAnimation = br.ReadUInt16();
            if (this.Flags1.ReadFlag(ItemDropFlags1.UseTime)) 
                this.UseTime = br.ReadUInt16();
            if (this.Flags1.ReadFlag(ItemDropFlags1.Shoot)) 
                this.Shoot = br.ReadInt16();
            if (this.Flags1.ReadFlag(ItemDropFlags1.ShootSpeed)) 
                this.ShootSpeed = br.ReadSingle();

            if (this.Flags1.ReadFlag(ItemDropFlags1.NextFlags))
            {
                this.Flags2 = br.ReadByte();
                if (this.Flags2.ReadFlag(ItemDropFlags2.Width))
                    this.Width = br.ReadInt16();
                if (this.Flags2.ReadFlag(ItemDropFlags2.Height))
                    this.Height = br.ReadInt16();
                if (this.Flags2.ReadFlag(ItemDropFlags2.Scale))
                    this.Scale = br.ReadSingle();
                if (this.Flags2.ReadFlag(ItemDropFlags2.Ammo))
                    this.Ammo = br.ReadInt16();
                if (this.Flags2.ReadFlag(ItemDropFlags2.UseAmmo))
                    this.UseAmmo = br.ReadInt16();
                if (this.Flags2.ReadFlag(ItemDropFlags2.NotAmmo))
                    this.NotAmmo = br.ReadBoolean();
            }
        }

        public override short GetLength()
        {
            short length = 3;

            if (this.Flags1.ReadFlag(ItemDropFlags1.Color))
                length += 4;
            if (this.Flags1.ReadFlag(ItemDropFlags1.Damage))
                length += 2;
            if (this.Flags1.ReadFlag(ItemDropFlags1.Knockback))
                length += 4;
            if (this.Flags1.ReadFlag(ItemDropFlags1.UseAnimation))
                length += 2;
            if (this.Flags1.ReadFlag(ItemDropFlags1.UseTime))
                length += 2;
            if (this.Flags1.ReadFlag(ItemDropFlags1.Shoot))
                length += 2;
            if (this.Flags1.ReadFlag(ItemDropFlags1.ShootSpeed))
                length += 4;

            if (this.Flags1.ReadFlag(ItemDropFlags1.NextFlags))
            {
                length += 1;
                if (this.Flags2.ReadFlag(ItemDropFlags2.Width))
                    length += 2;
                if (this.Flags2.ReadFlag(ItemDropFlags2.Height))
                    length += 2;
                if (this.Flags2.ReadFlag(ItemDropFlags2.Scale))
                    length += 4;
                if (this.Flags2.ReadFlag(ItemDropFlags2.Ammo))
                    length += 2;
                if (this.Flags2.ReadFlag(ItemDropFlags2.UseAmmo))
                    length += 2;
                if (this.Flags2.ReadFlag(ItemDropFlags2.NotAmmo))
                    length += 2;
            }

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

            using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(ItemIndex);
                bw.Write(Flags1);
                if (Flags1.ReadFlag(ItemDropFlags1.Color))
                    bw.Write(PackedColorValue);
                if (this.Flags1.ReadFlag(ItemDropFlags1.Damage))
                    bw.Write(Damage);
                if (this.Flags1.ReadFlag(ItemDropFlags1.Knockback))
                    bw.Write(Knockback);
                if (this.Flags1.ReadFlag(ItemDropFlags1.UseAnimation))
                    bw.Write(UseAnimation);
                if (this.Flags1.ReadFlag(ItemDropFlags1.UseTime))
                    bw.Write(UseTime);
                if (this.Flags1.ReadFlag(ItemDropFlags1.Shoot))
                    bw.Write(Shoot);
                if (this.Flags1.ReadFlag(ItemDropFlags1.ShootSpeed))
                    bw.Write(ShootSpeed);

                if (this.Flags1.ReadFlag(ItemDropFlags1.NextFlags))
                {
                    bw.Write(Flags2);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.Width))
                        bw.Write(Width);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.Height))
                        bw.Write(Height);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.Scale))
                        bw.Write(Scale);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.Ammo))
                        bw.Write(Ammo);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.UseAmmo))
                        bw.Write(UseAmmo);
                    if (this.Flags2.ReadFlag(ItemDropFlags2.NotAmmo))
                        bw.Write(NotAmmo);
                }
            }
        }

        public override string ToString()
        {
            return $"[AlterItemDrop ItemIndex: {ItemIndex}, Width: {Width}, Height: {Height}]";
        }
    }
}

