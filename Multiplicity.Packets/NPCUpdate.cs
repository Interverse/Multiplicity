using System;
using System.Collections.Generic;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    [Flags]
    public enum NPCUpdateFlags : byte
    {
        None = 0,
        DirectionX = 1,
        DirectionY = 1 << 1,
        AI3 = 1 << 2,
        AI2 = 1 << 3,
        AI1 = 1 << 4,
        AI0 = 1 << 5,
        SpriteDirection = 1 << 6,
        FullLife = 1 << 7
    }

    public class NPCUpdate : TerrariaPacket
    {
        public static readonly int[] NetIDMap = new int[]
        {
            81,81,1,1,1,1,1,1,1,1,6,6,31,31,77,42,42,176,176,176,176,173,173,183,183,3,
            3,132,132,186,186,187,187,188,188,189,189,190,191,192,193,194,2,200,200,21,
            21,201,201,202,202,203,203,223,223,231,231,232,232,233,233,234,234,235,235
        };
        public static readonly HashSet<short> npcCatchable = new HashSet<short>()
        {
            46,55,74,148,149,297,298,299,300,355,356,357,358,359,360,361,362,363,364,
            365,366,367,374,377,539,538,484,485,486,487,442,443,444,445,446,447,448
        };

        public short NPCID { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public ushort Target { get; set; }

        public byte Flags { get; set; }

        public float[] AI { get; set; }

        public short NPCNetID { get; set; }

        public byte LifeBytes { get; set; }

        public int Life { get; set; }

        public byte ReleaseOwner { get; set; }

        public int NPCType { get; set; }

        public NPCUpdate()
            : base((byte)PacketTypes.NPCUpdate)
        {

        }

        public NPCUpdate(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();
            this.VelocityX = br.ReadSingle();
            this.VelocityY = br.ReadSingle();
            this.Target = br.ReadUInt16();
            this.Flags = br.ReadByte();
            this.AI = new float[4];
            if (this.Flags.ReadBit(2))
                this.AI[0] = br.ReadSingle();
            if (this.Flags.ReadBit(3))
                this.AI[1] = br.ReadSingle();
            if (this.Flags.ReadBit(4))
                this.AI[2] = br.ReadSingle();
            if (this.Flags.ReadBit(5))
                this.AI[3] = br.ReadSingle();
            this.NPCNetID = br.ReadInt16();
            if (!this.Flags.ReadBit(7))
            {
                this.LifeBytes = br.ReadByte();
                if (this.LifeBytes == 1)
                    this.Life = br.ReadByte();
                else if (this.LifeBytes == 2)
                    this.Life = br.ReadInt16();
                else
                    this.Life = br.ReadInt32();
            }
            this.NPCType = NPCTypeFromNetID(this.NPCNetID);
            if (this.NPCType >= 0 && this.NPCType < 580 && npcCatchable.Contains((short)this.NPCType))
                this.ReleaseOwner = br.ReadByte();
        }

        public static int NPCTypeFromNetID(int id)
        {
            if (id < 0)
            {
                return NetIDMap[-id - 1];
            }
            return id;
        }

        public override short GetLength()
        {
            short length = 23;

            if (this.Flags.ReadBit(2))
                length++;
            if (this.Flags.ReadBit(3))
                length++;
            if (this.Flags.ReadBit(4))
                length++;
            if (this.Flags.ReadBit(5))
                length++;
            if (!this.Flags.ReadBit(7))
                length += (short)(1 + this.LifeBytes);
            this.NPCType = NPCTypeFromNetID(this.NPCNetID);
            if (this.NPCType >= 0 && this.NPCType < 580 && npcCatchable.Contains((short)this.NPCType))
                length++;

            return length;
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            base.ToStream(stream, includeHeader);

            using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(this.NPCID);
                bw.Write(this.PositionX);
                bw.Write(this.PositionY);
                bw.Write(this.VelocityX);
                bw.Write(this.VelocityY);
                bw.Write(this.Target);
                bw.Write(this.Flags);
                if (this.Flags.ReadBit(2))
                    bw.Write(this.AI[0]);
                if (this.Flags.ReadBit(3))
                    bw.Write(this.AI[1]);
                if (this.Flags.ReadBit(4))
                    bw.Write(this.AI[2]);
                if (this.Flags.ReadBit(5))
                    bw.Write(this.AI[3]);
                bw.Write(this.NPCNetID);
                if (!this.Flags.ReadBit(7))
                {
                    bw.Write(this.LifeBytes);
                    if (this.LifeBytes == 1)
                        bw.Write((byte)this.Life);
                    else if (this.LifeBytes == 2)
                        bw.Write((short)this.Life);
                    else
                        bw.Write((int)this.Life);
                }
                this.NPCType = NPCTypeFromNetID(this.NPCNetID);
                if (this.NPCType >= 0 && this.NPCType < 580 && npcCatchable.Contains((short)this.NPCType))
                    bw.Write(this.ReleaseOwner);
            }
        }

        public override string ToString()
        {
            return string.Format("[NPCUpdate: NPCID={0}, PositionX={1}, PositionY={2}, VelocityX={3}, VelocityY={4}, Target={5}, Flags={6}]",
                NPCID, PositionX, PositionY, VelocityX, VelocityY, Target, Flags);
        }
    }
}

