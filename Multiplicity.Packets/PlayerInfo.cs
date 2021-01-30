using System;
using System.IO;
using Multiplicity.Packets.Extensions;
using Multiplicity.Packets.BitFlags;
using System.Drawing;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerInfo (0x4) packet.
    /// </summary>
    public class PlayerInfo : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte SkinVarient { get; set; }

        /// <summary>
        /// Gets or sets the Hair - If >134 then Set To 0|
        /// </summary>
        public byte Hair { get; set; }

        public string Name { get; set; }

        public byte HairDye { get; set; }

        public byte HideVisuals { get; set; }

        public byte HideVisuals2 { get; set; }

        public byte HideMisc { get; set; }

        public ColorStruct HairColor { get; set; }

        public ColorStruct SkinColor { get; set; }

        public ColorStruct EyeColor { get; set; }

        public ColorStruct ShirtColor { get; set; }

        public ColorStruct UnderShirtColor { get; set; }

        public ColorStruct PantsColor { get; set; }

        public ColorStruct ShoeColor { get; set; }

        /// <summary>
        /// Sets or Gets Difficulty - See <see cref="DifficultyFlags"/> for byte list
        /// </summary>
        public byte Difficulty { get; set; }

        /// <summary>
        /// Sets or Gets Torch - See <see cref="TorchFlags"/> for byte list
        /// </summary>
        public byte Torch { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        public PlayerInfo()
            : base((byte)PacketTypes.PlayerInfo)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerInfo(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.SkinVarient = br.ReadByte();
            this.Hair = br.ReadByte();
            this.Name = br.ReadString();
            this.HairDye = br.ReadByte();
            this.HideVisuals = br.ReadByte();
            this.HideVisuals2 = br.ReadByte();
            this.HideMisc = br.ReadByte();
            this.HairColor = br.ReadColor();
            this.SkinColor = br.ReadColor();
            this.EyeColor = br.ReadColor();
            this.ShirtColor = br.ReadColor();
            this.UnderShirtColor = br.ReadColor();
            this.PantsColor = br.ReadColor();
            this.ShoeColor = br.ReadColor();
            this.Difficulty = br.ReadByte();
            this.Torch = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PlayerInfo: PlayerID = {PlayerID} SkinVarient = {SkinVarient} Hair = {Hair} Name = {Name} HairDye = {HairDye} HideVisuals = {HideVisuals} HideVisuals2 = {HideVisuals2} HideMisc = {HideMisc} HairColor = {HairColor} SkinColor = {SkinColor} EyeColor = {EyeColor} ShirtColor = {ShirtColor} UnderShirtColor = {UnderShirtColor} PantsColor = {PantsColor} ShoeColor = {ShoeColor} Difficulty = {Difficulty} Torch = {Torch}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(31 + Name.Length);
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
                br.Write(PlayerID);
                br.Write(SkinVarient);
                br.Write(Hair);
                br.Write(Name);
                br.Write(HairDye);
                br.Write(HideVisuals);
                br.Write(HideVisuals2);
                br.Write(HideMisc);
                br.Write(HairColor);
                br.Write(SkinColor);
                br.Write(EyeColor);
                br.Write(ShirtColor);
                br.Write(UnderShirtColor);
                br.Write(PantsColor);
                br.Write(ShoeColor);
                br.Write(Difficulty);
                br.Write(Torch);
            }
        }

        #endregion

    }
}
