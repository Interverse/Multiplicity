using System.IO;
using Multiplicity.Packets.BitFlags;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerDeathV2 (76) packet.
    /// </summary>
    public class PlayerDeathV2 : TerrariaPacket
    {
        public byte PlayerID { get; set; }

        /// <summary>
        /// BitFlags: 1 = Killed via PvP, 2 = Killed via NPC, 4 = Killed via Projectile, 8 = Killed via Other, 
        /// 16 = Killed via Projectile, 32 = Killed via PvP, 64 = Killed via PvP, 128 = Killed via Custom Modification
        /// </summary>
        public byte PlayerDeathReason { get; set; }

        /// <summary>
        /// Only in PvP. (Only if BitFlags[0] is true)
        /// </summary>
        public short FromPlayerIndex { get; set; } = -1;

        /// <summary>
        /// Only if hurt by an npc. (Only if BitFlags[1] is true)
        /// </summary>
        public short FromNpcIndex { get; set; } = -1;

        /// <summary>
        /// Only if killed by Projectile. (Only if BitFlags[2] is true)
        /// </summary>
        public short FromProjectileIndex { get; set; } = -1;

        /// <summary>
        /// Only if Killed by Other (Only if BitFlags[3]) - 
        /// 0 = Fall damage, 1 = Drowning, 2 = Lava damage, 3 = Fall damage, 4 = Demon Altar,
        /// 5 = N/A, 6 = Companion Cube, 7 = Suffocation, 8 = Burning, 9 = Poison/Venom,
        /// 10 = Electrified, 11 = WoF (escaped), 12 = WoF (licked), 13 = Chaos State,
        /// 14 = Chaos State V2 (male), 15 = Chaos State V2 (female)
        /// 254 = nothing
        /// </summary>
        public byte FromOther { get; set; } = 254;

        /// <summary>
        /// Only only is killed via projectile. (Only if BitFlags[4] is true)
        /// </summary>
        public short FromProjectileType { get; set; }

        /// <summary>
        /// Only in PvP. (Only if BitFlags[5] is true)
        /// </summary>
        public short FromItemType { get; set; }

        /// <summary>
        /// Only in PvP. (Only if BitFlags[6] is true)
        /// </summary>
        public byte FromItemPrefix { get; set; }

        /// <summary>
        /// Only if killed via Custom Modification (Only if BitFlags[7] is true)
        /// </summary>
        public string FromCustomReason { get; set; }

        public short Damage { get; set; }

        public byte HitDirection { get; set; }

        /// <summary>
        /// BitFlags: 1 = PvP
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        public PlayerDeathV2()
            : base((byte)PacketTypes.PlayerDeathV2)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerDeathV2(BinaryReader br)
            : base(br)
        {
            PlayerID = br.ReadByte();
            PlayerDeathReason = br.ReadByte();

            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP))
                FromPlayerIndex = br.ReadInt16();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaNPC))
                FromNpcIndex = br.ReadInt16();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile))
                FromProjectileIndex = br.ReadInt16();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaOther))
                FromOther = br.ReadByte();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile2))
                FromProjectileType = br.ReadInt16();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP2))
                FromItemType = br.ReadInt16();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP3))
                FromItemPrefix = br.ReadByte();
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaCustomModification))
                FromCustomReason = br.ReadString();

            Damage = br.ReadInt16();
            HitDirection = br.ReadByte();
            Flags = br.ReadByte();
        }

        public override string ToString()
        {
            return
                $"[PlayerDeathV2: PlayerId = {PlayerID} PlayerDeathReason = {PlayerDeathReason} FromPlayerIndex = {FromPlayerIndex} FromNpcIndex = {FromNpcIndex} FromProjectileIndex = {FromProjectileIndex} FromOther = {FromOther} FromProjectileType = {FromProjectileType} FromItemType = {FromItemType} FromItemPrefix = {FromItemPrefix} FromCustomReason = {FromCustomReason} Damage = {Damage} HitDirection = {HitDirection} Flags = {Flags}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            int _packetLength = 0;
            if (PlayerDeathReason.ReadBit(0))
                _packetLength += 2;
            if (PlayerDeathReason.ReadBit(1))
                _packetLength += 2;
            if (PlayerDeathReason.ReadBit(2))
                _packetLength += 2;
            if (PlayerDeathReason.ReadBit(3))
                _packetLength += 1;
            if (PlayerDeathReason.ReadBit(4))
                _packetLength += 2;
            if (PlayerDeathReason.ReadBit(5))
                _packetLength += 2;
            if (PlayerDeathReason.ReadBit(6))
                _packetLength += 1;
            if (PlayerDeathReason.ReadBit(7))
                _packetLength += 1 + FromCustomReason.Length;
            return (short)(6 + _packetLength);
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader) {
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
            using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true)) {
                br.Write(PlayerID);
                br.Write(PlayerDeathReason);

                if (FromPlayerIndex != -1)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaPVP, true);
                    br.Write(FromPlayerIndex);
                }

                if (FromNpcIndex != -1)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaNPC, true);
                    br.Write(FromNpcIndex);
                }

                if (FromProjectileIndex != -1)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaProjectile, true);
                    br.Write(FromProjectileIndex);
                }

                if (FromOther != 254)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaOther, true);
                    br.Write(FromOther);
                }

                if (FromProjectileType != 0)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaProjectile2, true);
                    br.Write(FromProjectileType);
                }

                if (FromItemType != 0)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaPVP2, true);
                    br.Write(FromItemType);
                }

                if (FromItemPrefix != 0)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaPVP3, true);
                    br.Write(FromItemPrefix);
                }

                if (FromCustomReason != null)
                {
                    PlayerDeathReason = PlayerDeathReason.SetFlag(PlayerDeathReasonFlags.KilledViaCustomModification, true);
                    br.Write(FromCustomReason);
                }

                br.Write(Damage);
                br.Write(HitDirection);
                br.Write(Flags);
            }
        }

        #endregion
    }
}
