using System;
using System.Collections.Generic;
using System.IO;
using Multiplicity.Packets.BitFlags;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
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
            365,366,367,374,377,539,538,484,485,486,487,442,443,444,445,446,447,448,
            538,583,584,585,592,593,595,596,597,598,599,600,601,602,603,604,605,607,
            608,609,610,611,612,613,614,615,616,617,625,627,628,639,640,641,642,643,
            644,645,646,647,648,649,650,651,652,653,654,606,655,661
        };

        public short NPCID { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public ushort Target { get; set; }

        public byte NpcFlags1 { get; set; }

        public byte NpcFlags2 { get; set; }

        public float[] AI { get; set; }

        public short NPCNetID { get; set; }

        public byte PlayerCountForMultiplayerDifficultyOverride { get; set; }

        public float StrengthMultiplier { get; set; }

        public byte LifeBytes { get; set; }

        public int Life { get; set; }

        public byte ReleaseOwner { get; set; }

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
            this.NpcFlags1 = br.ReadByte();
            this.NpcFlags2 = br.ReadByte();
            this.AI = new float[4];
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI0))
                this.AI[0] = br.ReadSingle();
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI1))
                this.AI[1] = br.ReadSingle();
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI2))
                this.AI[2] = br.ReadSingle();
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI3))
                this.AI[3] = br.ReadSingle();
            this.NPCNetID = br.ReadInt16();
            if (this.NpcFlags2.ReadFlag(NPCUpdateFlags2.StatsScaled))
                this.PlayerCountForMultiplayerDifficultyOverride = br.ReadByte();
            if (this.NpcFlags2.ReadFlag(NPCUpdateFlags2.StrengthMultiplier))
                this.StrengthMultiplier = br.ReadSingle();
            if (!this.NpcFlags1.ReadFlag(NPCUpdateFlags.LifeMax))
            {
                this.LifeBytes = br.ReadByte();
                if (this.LifeBytes == 1)
                    this.Life = br.ReadByte();
                else if (this.LifeBytes == 2)
                    this.Life = br.ReadInt16();
                else
                    this.Life = br.ReadInt32();
            }
            if (this.NPCNetID >= 0 && this.NPCNetID < 665 && npcCatchable.Contains((short)this.NPCNetID))
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
            short length = 24;

            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI0))
                length += 4;
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI1))
                length += 4;
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI2))
                length += 4;
            if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI3))
                length += 4;
            if (!this.NpcFlags1.ReadFlag(NPCUpdateFlags.LifeMax))
                length += (short)(1 + this.LifeBytes);
            if (this.NPCNetID >= 0 && this.NPCNetID < 665 && npcCatchable.Contains((short)this.NPCNetID))
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
                bw.Write(this.NpcFlags1);
                bw.Write(this.NpcFlags2);
                if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI0))
                    bw.Write(this.AI[0]);
                if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI1))
                    bw.Write(this.AI[1]);
                if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI2))
                    bw.Write(this.AI[2]);
                if (this.NpcFlags1.ReadFlag(NPCUpdateFlags.AI3))
                    bw.Write(this.AI[3]);
                bw.Write(this.NPCNetID);
                if (this.NpcFlags2.ReadFlag(NPCUpdateFlags2.StatsScaled))
                    bw.Write(this.PlayerCountForMultiplayerDifficultyOverride);
                if (this.NpcFlags2.ReadFlag(NPCUpdateFlags2.StrengthMultiplier))
                    bw.Write(this.StrengthMultiplier);
                if (!this.NpcFlags1.ReadFlag(NPCUpdateFlags.LifeMax))
                {
                    bw.Write(this.LifeBytes);
                    if (this.LifeBytes == 1)
                        bw.Write((byte)this.Life);
                    else if (this.LifeBytes == 2)
                        bw.Write((short)this.Life);
                    else
                        bw.Write((int)this.Life);
                }
                if (this.NPCNetID >= 0 && this.NPCNetID < 665 && npcCatchable.Contains((short)this.NPCNetID))
                    bw.Write(this.ReleaseOwner);
            }
        }

        public override string ToString()
        {
            return string.Format("[NPCUpdate: NPCID={0}, PositionX={1}, PositionY={2}, VelocityX={3}, VelocityY={4}, Target={5}, Flags={6}, Flags2={7}]",
                NPCID, PositionX, PositionY, VelocityX, VelocityY, Target, NpcFlags1, NpcFlags2);
        }
    }
}

