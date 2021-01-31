using System.IO;
using Multiplicity.Packets.BitFlags;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerHurtV2 (75) packet.
    /// </summary>
    public class PlayerHurtV2 : TerrariaPacket
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
        /// BitFlags: 1 = Crit, 2 = PvP
        /// </summary>
        public byte Flags { get; set; }

        public sbyte CooldownCounter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        public PlayerHurtV2()
            : base((byte)PacketTypes.PlayerHurtV2)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerHurtV2(BinaryReader br)
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
            CooldownCounter = br.ReadSByte();
        }

        public override string ToString()
        {
            return
                $"[PlayerHurtV2: PlayerId = {PlayerID} PlayerDeathReason = {PlayerDeathReason} FromPlayerIndex = {FromPlayerIndex} FromNpcIndex = {FromNpcIndex} FromProjectileIndex = {FromProjectileIndex} FromOther = {FromOther} FromProjectileType = {FromProjectileType} FromItemType = {FromItemType} FromItemPrefix = {FromItemPrefix} FromCustomReason = {FromCustomReason} Damage = {Damage} HitDirection = {HitDirection} Flags = {Flags} CooldownCounter = {CooldownCounter}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            int _packetLength = 0;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP))
                _packetLength += 2;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaNPC))
                _packetLength += 2;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile))
                _packetLength += 2;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaOther))
                _packetLength += 1;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile2))
                _packetLength += 2;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP2))
                _packetLength += 2;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP3))
                _packetLength += 1;
            if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaCustomModification))
                _packetLength += FromCustomReason.Length;
            return (short)(7 + _packetLength);
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

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP))
                {
                    br.Write(FromPlayerIndex);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaNPC))
                {
                    br.Write(FromNpcIndex);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile))
                {
                    br.Write(FromProjectileIndex);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaOther))
                {
                    br.Write(FromOther);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaProjectile2))
                {
                    br.Write(FromProjectileType);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP2))
                {
                    br.Write(FromItemType);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaPVP3))
                {
                    br.Write(FromItemPrefix);
                }

                if (PlayerDeathReason.ReadFlag(PlayerDeathReasonFlags.KilledViaCustomModification))
                {
                    br.Write(FromCustomReason);
                }

                br.Write(Damage);
                br.Write(HitDirection);
                br.Write(Flags);
                br.Write(CooldownCounter);
            }
        }

        #endregion
    }
}
