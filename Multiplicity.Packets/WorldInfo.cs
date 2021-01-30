using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The WorldInfo (0x7) packet.
    /// </summary>
    public class WorldInfo : TerrariaPacket
    {

        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the DayandMoonInfo - See <see cref="DayAndMoonInfoFlags"/> for byte list|
        /// </summary>
        public byte DayandMoonInfo { get; set; }

        public byte MoonPhase { get; set; }

        public short MaxTilesX { get; set; }

        public short MaxTilesY { get; set; }

        public short SpawnX { get; set; }

        public short SpawnY { get; set; }

        public short WorldSurface { get; set; }

        public short RockLayer { get; set; }

        public int WorldID { get; set; }

        public string WorldName { get; set; }

        public byte GameMode { get; set; }

        public byte[] WorldUniqueID { get; set; }

        public ulong WorldGeneratorVersion { get; set; }

        public byte MoonType { get; set; }

        public byte TreeBackground { get; set; }

        public byte CorruptionBackground { get; set; }

        public byte JungleBackground { get; set; }

        public byte SnowBackground { get; set; }

        public byte HallowBackground { get; set; }

        public byte CrimsonBackground { get; set; }

        public byte DesertBackground { get; set; }

        public byte OceanBackground { get; set; }

        public byte UnknownBackground1 { get; set; }

        public byte UnknownBackground2 { get; set; }

        public byte UnknownBackground3 { get; set; }

        public byte UnknownBackground4 { get; set; }

        public byte UnknownBackground5 { get; set; }

        public byte IceBackStyle { get; set; }

        public byte JungleBackStyle { get; set; }

        public byte HellBackStyle { get; set; }

        public float WindSpeedSet { get; set; }

        public byte CloudNumber { get; set; }

        public int Tree1 { get; set; }

        public int Tree2 { get; set; }

        public int Tree3 { get; set; }

        public byte TreeStyle1 { get; set; }

        public byte TreeStyle2 { get; set; }

        public byte TreeStyle3 { get; set; }

        public byte TreeStyle4 { get; set; }

        public int CaveBack1 { get; set; }

        public int CaveBack2 { get; set; }

        public int CaveBack3 { get; set; }

        public byte CaveBackStyle1 { get; set; }

        public byte CaveBackStyle2 { get; set; }

        public byte CaveBackStyle3 { get; set; }

        public byte CaveBackStyle4 { get; set; }

        public byte Forest1TreeTopStyle { get; set; }

        public byte Forest2TreeTopStyle { get; set; }

        public byte Forest3TreeTopStyle { get; set; }

        public byte Forest4TreeTopStyle { get; set; }

        public byte CorruptionTreeTopStyle { get; set; }

        public byte JungleTreeTopStyle { get; set; }

        public byte SnowTreeTopStyle { get; set; }

        public byte HallowTreeTopStyle { get; set; }

        public byte CrimsonTreeTopStyle { get; set; }

        public byte DesertTreeTopStyle { get; set; }

        public byte OceanTreeTopStyle { get; set; }

        public byte GlowingMushroomTreeTopStyle { get; set; }

        public byte UnderworldTreeTopStyle { get; set; }

        public float Rain { get; set; }

        /// <summary>
        /// See <see cref="EventInfoFlags"/> for the byte list
        /// </summary>
        public byte EventInfo { get; set; }

        /// <summary>
        /// See <see cref="EventInfo2Flags"/> for the byte list
        /// </summary>
        public byte EventInfo2 { get; set; }

        /// <summary>
        /// See <see cref="EventInfo3Flags"/> for the byte list
        /// </summary>
        public byte EventInfo3 { get; set; }

        /// <summary>
        /// See <see cref="EventInfo4Flags"/> for the byte list
        /// </summary>
        public byte EventInfo4 { get; set; }

        /// <summary>
        /// See <see cref="EventInfo5Flags"/> for the byte list
        /// </summary>
        public byte EventInfo5 { get; set; }

        /// <summary>
        /// See <see cref="EventInfo6Flags"/> for the byte list
        /// </summary>
        public byte EventInfo6 { get; set; }

        /// <summary>
        /// See <see cref="EventInfo7Flags"/> for the byte list
        /// </summary>
        public byte EventInfo7 { get; set; }

        /// <summary>
        /// Either Tile ID 7 or 166
        /// </summary>
        public short CopperOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 6 or 167
        /// </summary>
        public short IronOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 9 or 168
        /// </summary>
        public short SilverOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 8 or 169
        /// </summary>
        public short GoldOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 107 or 221
        /// </summary>
        public short CobaltOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 108 or 222
        /// </summary>
        public short MythrilOreTier { get; set; }

        /// <summary>
        /// Either Tile ID 111 or 223
        /// </summary>
        public short AdamantiteOreTier { get; set; }

        public sbyte InvasionType { get; set; }

        public ulong LobbyID { get; set; }

        public float SandstormSeverity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldInfo"/> class.
        /// </summary>
        public WorldInfo()
            : base((byte)PacketTypes.WorldInfo)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldInfo"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public WorldInfo(BinaryReader br)
            : base(br)
        {
            this.Time = br.ReadInt32();
            this.DayandMoonInfo = br.ReadByte();
            this.MoonPhase = br.ReadByte();
            this.MaxTilesX = br.ReadInt16();
            this.MaxTilesY = br.ReadInt16();
            this.SpawnX = br.ReadInt16();
            this.SpawnY = br.ReadInt16();
            this.WorldSurface = br.ReadInt16();
            this.RockLayer = br.ReadInt16();
            this.WorldID = br.ReadInt32();
            this.WorldName = br.ReadString();
            this.GameMode = br.ReadByte();
            this.WorldUniqueID = br.ReadBytes(16);
            this.WorldGeneratorVersion = br.ReadUInt64();
            this.MoonType = br.ReadByte();
            this.TreeBackground = br.ReadByte();
            this.CorruptionBackground = br.ReadByte();
            this.JungleBackground = br.ReadByte();
            this.SnowBackground = br.ReadByte();
            this.HallowBackground = br.ReadByte();
            this.CrimsonBackground = br.ReadByte();
            this.DesertBackground = br.ReadByte();
            this.OceanBackground = br.ReadByte();
            this.UnknownBackground1 = br.ReadByte();
            this.UnknownBackground2 = br.ReadByte();
            this.UnknownBackground3 = br.ReadByte();
            this.UnknownBackground4 = br.ReadByte();
            this.UnknownBackground5 = br.ReadByte();
            this.IceBackStyle = br.ReadByte();
            this.JungleBackStyle = br.ReadByte();
            this.HellBackStyle = br.ReadByte();
            this.WindSpeedSet = br.ReadSingle();
            this.CloudNumber = br.ReadByte();
            this.Tree1 = br.ReadInt32();
            this.Tree2 = br.ReadInt32();
            this.Tree3 = br.ReadInt32();
            this.TreeStyle1 = br.ReadByte();
            this.TreeStyle2 = br.ReadByte();
            this.TreeStyle3 = br.ReadByte();
            this.TreeStyle4 = br.ReadByte();
            this.CaveBack1 = br.ReadInt32();
            this.CaveBack2 = br.ReadInt32();
            this.CaveBack3 = br.ReadInt32();
            this.CaveBackStyle1 = br.ReadByte();
            this.CaveBackStyle2 = br.ReadByte();
            this.CaveBackStyle3 = br.ReadByte();
            this.CaveBackStyle4 = br.ReadByte();
            this.Forest1TreeTopStyle = br.ReadByte();
            this.Forest2TreeTopStyle = br.ReadByte();
            this.Forest3TreeTopStyle = br.ReadByte();
            this.Forest4TreeTopStyle = br.ReadByte();
            this.CorruptionTreeTopStyle = br.ReadByte();
            this.JungleTreeTopStyle = br.ReadByte();
            this.SnowTreeTopStyle = br.ReadByte();
            this.HallowTreeTopStyle = br.ReadByte();
            this.CrimsonTreeTopStyle = br.ReadByte();
            this.DesertTreeTopStyle = br.ReadByte();
            this.OceanTreeTopStyle = br.ReadByte();
            this.GlowingMushroomTreeTopStyle = br.ReadByte();
            this.UnderworldTreeTopStyle = br.ReadByte();
            this.Rain = br.ReadSingle();
            this.EventInfo = br.ReadByte();
            this.EventInfo2 = br.ReadByte();
            this.EventInfo3 = br.ReadByte();
            this.EventInfo4 = br.ReadByte();
            this.EventInfo5 = br.ReadByte();
            this.EventInfo6 = br.ReadByte();
            this.EventInfo7 = br.ReadByte();
            this.CopperOreTier = br.ReadInt16();
            this.IronOreTier = br.ReadInt16();
            this.SilverOreTier = br.ReadInt16();
            this.GoldOreTier = br.ReadInt16();
            this.CobaltOreTier = br.ReadInt16();
            this.MythrilOreTier = br.ReadInt16();
            this.AdamantiteOreTier = br.ReadInt16();
            this.InvasionType = br.ReadSByte();
            this.LobbyID = br.ReadUInt64();
            this.SandstormSeverity = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[WorldInfo: Time = {Time} DayandMoonInfo = {DayandMoonInfo} MoonPhase = {MoonPhase} MaxTilesX = {MaxTilesX} MaxTilesY = {MaxTilesY} SpawnX = {SpawnX} SpawnY = {SpawnY} WorldSurface = {WorldSurface} RockLayer = {RockLayer} WorldID = {WorldID} WorldName = {WorldName} WorldUniqueID = {WorldUniqueID} WorldGeneratorVersion = {WorldGeneratorVersion} MoonType = {MoonType} TreeBackground = {TreeBackground} CorruptionBackground = {CorruptionBackground} JungleBackground = {JungleBackground} SnowBackground = {SnowBackground} HallowBackground = {HallowBackground} CrimsonBackground = {CrimsonBackground} DesertBackground = {DesertBackground} OceanBackground = {OceanBackground} IceBackStyle = {IceBackStyle} JungleBackStyle = {JungleBackStyle} HellBackStyle = {HellBackStyle} WindSpeedSet = {WindSpeedSet} CloudNumber = {CloudNumber} Tree1 = {Tree1} Tree2 = {Tree2} Tree3 = {Tree3} TreeStyle1 = {TreeStyle1} TreeStyle2 = {TreeStyle2} TreeStyle3 = {TreeStyle3} TreeStyle4 = {TreeStyle4} CaveBack1 = {CaveBack1} CaveBack2 = {CaveBack2} CaveBack3 = {CaveBack3} CaveBackStyle1 = {CaveBackStyle1} CaveBackStyle2 = {CaveBackStyle2} CaveBackStyle3 = {CaveBackStyle3} CaveBackStyle4 = {CaveBackStyle4} Rain = {Rain} EventInfo = {EventInfo} EventInfo2 = {EventInfo2} EventInfo3 = {EventInfo3} EventInfo4 = {EventInfo4} EventInfo5 = {EventInfo5} InvasionType = {InvasionType} LobbyID = {LobbyID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(187 + WorldName.Length);
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
                br.Write(Time);
                br.Write(DayandMoonInfo);
                br.Write(MoonPhase);
                br.Write(MaxTilesX);
                br.Write(MaxTilesY);
                br.Write(SpawnX);
                br.Write(SpawnY);
                br.Write(WorldSurface);
                br.Write(RockLayer);
                br.Write(WorldID);
                br.Write(WorldName);
                br.Write(GameMode);
                br.Write(WorldUniqueID);
                br.Write(WorldGeneratorVersion);
                br.Write(MoonType);
                br.Write(TreeBackground);
                br.Write(CorruptionBackground);
                br.Write(JungleBackground);
                br.Write(SnowBackground);
                br.Write(HallowBackground);
                br.Write(CrimsonBackground);
                br.Write(DesertBackground);
                br.Write(OceanBackground);
                br.Write(UnknownBackground1);
                br.Write(UnknownBackground2);
                br.Write(UnknownBackground3);
                br.Write(UnknownBackground4);
                br.Write(UnknownBackground5);
                br.Write(IceBackStyle);
                br.Write(JungleBackStyle);
                br.Write(HellBackStyle);
                br.Write(WindSpeedSet);
                br.Write(CloudNumber);
                br.Write(Tree1);
                br.Write(Tree2);
                br.Write(Tree3);
                br.Write(TreeStyle1);
                br.Write(TreeStyle2);
                br.Write(TreeStyle3);
                br.Write(TreeStyle4);
                br.Write(CaveBack1);
                br.Write(CaveBack2);
                br.Write(CaveBack3);
                br.Write(CaveBackStyle1);
                br.Write(CaveBackStyle2);
                br.Write(CaveBackStyle3);
                br.Write(CaveBackStyle4);
                br.Write(Forest1TreeTopStyle);
                br.Write(Forest2TreeTopStyle);
                br.Write(Forest3TreeTopStyle);
                br.Write(Forest4TreeTopStyle);
                br.Write(CorruptionTreeTopStyle);
                br.Write(JungleTreeTopStyle);
                br.Write(SnowTreeTopStyle);
                br.Write(HallowTreeTopStyle);
                br.Write(CrimsonTreeTopStyle);
                br.Write(DesertTreeTopStyle);
                br.Write(OceanTreeTopStyle);
                br.Write(GlowingMushroomTreeTopStyle);
                br.Write(UnderworldTreeTopStyle);
                br.Write(Rain);
                br.Write(EventInfo);
                br.Write(EventInfo2);
                br.Write(EventInfo3);
                br.Write(EventInfo4);
                br.Write(EventInfo5);
                br.Write(EventInfo6);
                br.Write(EventInfo7);
                br.Write(CopperOreTier);
                br.Write(IronOreTier);
                br.Write(SilverOreTier);
                br.Write(GoldOreTier);
                br.Write(CobaltOreTier);
                br.Write(MythrilOreTier);
                br.Write(AdamantiteOreTier);
                br.Write(InvasionType);
                br.Write(LobbyID);
                br.Write(SandstormSeverity);
            }
        }

        #endregion

    }
}
