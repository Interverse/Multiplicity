using System;
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
        public short NPCID { get; protected set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public ushort Target { get; set; }

        public byte Flags { get; set; }

        public byte[] Remainder { get; set; }

        public NPCUpdate()
            : base((byte)PacketTypes.NPCUpdate)
        {
            this.NPCID = NPCID;
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

            /*
			 * This is a fucking filthy hack, have to take stream length
			 * as a way to work out how much packet buffer we have left
			 * because the Terraria process has a runtime dictionary of NPC
			 * life bytes which tells the packet processor how many bytes of
			 * the NPC life there is in the packet.
			 * 
			 * We don't have access to this information short of blurting
			 * up our on dictionary of NPC life bytes in which I would rather
			 * kill myself than do.
			 */

            if (br.BaseStream.Length - br.BaseStream.Position > 0)
            {
                this.Remainder = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            }
        }

        public override short GetLength()
        {
            short fixedLen = 21;

            /*
			 * Dynamic packet sizes fucking suck balls
			 */

            fixedLen += (short)Remainder.Length;

            return fixedLen;
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
                bw.Write(Remainder);
            }
        }

        public override string ToString()
        {
            return string.Format("[NPCUpdate: NPCID={0}, PositionX={1}, PositionY={2}, VelocityX={3}, VelocityY={4}, Target={5}, Flags={6}]",
                NPCID, PositionX, PositionY, VelocityX, VelocityY, Target, Flags);
        }
    }
}

