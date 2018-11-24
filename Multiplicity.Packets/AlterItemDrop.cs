//
//  AlterItemDrop.cs
//
//  Author:
//       Josh Harris <celant@celantinteractive.com>
//
//  Copyright (c) 2016 Celant

using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    public class AlterItemDrop : TerrariaPacket
    {
        public short ItemIndex { get; set; }

        public byte Flags1 { get; set; }

        public uint PackedColorValue { get; set; }

        public ushort Damage { get; set; }

        public float Knockback { get; set; }

        public ushort UseAnimation { get; set; }

        public ushort UseTime { get; set; }

        public short Shoot { get; set; }

        public float ShootSpeed { get; set; }

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
            if (this.Flags1.ReadBit(0))
                this.PackedColorValue = br.ReadUInt32();
            if (this.Flags1.ReadBit(1))
                this.Damage = br.ReadUInt16();
            if (this.Flags1.ReadBit(2))
                this.Knockback = br.ReadSingle();
            if (this.Flags1.ReadBit(3)) 
                this.UseAnimation = br.ReadUInt16();
            if (this.Flags1.ReadBit(4)) 
                this.UseTime = br.ReadUInt16();
            if (this.Flags1.ReadBit(5)) 
                this.Shoot = br.ReadInt16();
            if (this.Flags1.ReadBit(6)) 
                this.ShootSpeed = br.ReadSingle();

            if (this.Flags1.ReadBit(7))
            {
                this.Flags2 = br.ReadByte();
                if (this.Flags2.ReadBit(0))
                    this.Width = br.ReadInt16();
                if (this.Flags2.ReadBit(1))
                    this.Height = br.ReadInt16();
                if (this.Flags2.ReadBit(2))
                    this.Scale = br.ReadSingle();
                if (this.Flags2.ReadBit(3))
                    this.Ammo = br.ReadInt16();
                if (this.Flags2.ReadBit(4))
                    this.UseAmmo = br.ReadInt16();
                if (this.Flags2.ReadBit(5))
                    this.NotAmmo = br.ReadBoolean();
            }
        }

        public override short GetLength()
        {
            short length = 3;

            if (this.Flags1.ReadBit(0))
                length += 4;
            if (this.Flags1.ReadBit(1))
                length += 2;
            if (this.Flags1.ReadBit(2))
                length += 4;
            if (this.Flags1.ReadBit(3))
                length += 2;
            if (this.Flags1.ReadBit(4))
                length += 2;
            if (this.Flags1.ReadBit(5))
                length += 2;
            if (this.Flags1.ReadBit(6))
                length += 4;

            if (this.Flags1.ReadBit(7))
            {
                length += 1;
                if (this.Flags2.ReadBit(0))
                    length += 2;
                if (this.Flags2.ReadBit(1))
                    length += 2;
                if (this.Flags2.ReadBit(2))
                    length += 4;
                if (this.Flags2.ReadBit(3))
                    length += 2;
                if (this.Flags2.ReadBit(4))
                    length += 2;
                if (this.Flags2.ReadBit(5))
                    length += 1;
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
                if (Flags1.ReadBit(1))
                    bw.Write(PackedColorValue);
                if (this.Flags1.ReadBit(2))
                    bw.Write(Damage);
                if (this.Flags1.ReadBit(4))
                    bw.Write(Knockback);
                if (this.Flags1.ReadBit(8))
                    bw.Write(UseAnimation);
                if (this.Flags1.ReadBit(16))
                    bw.Write(UseTime);
                if (this.Flags1.ReadBit(32))
                    bw.Write(Shoot);
                if (this.Flags1.ReadBit(64))
                    bw.Write(ShootSpeed);

                if (this.Flags1.ReadBit(128))
                {
                    bw.Write(Flags2);
                    if (this.Flags2.ReadBit(1))
                        bw.Write(Width);
                    if (this.Flags2.ReadBit(1))
                        bw.Write(Height);
                    if (this.Flags2.ReadBit(1))
                        bw.Write(Scale);
                    if (this.Flags2.ReadBit(1))
                        bw.Write(Ammo);
                    if (this.Flags2.ReadBit(1))
                        bw.Write(UseAmmo);
                    if (this.Flags2.ReadBit(1))
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

