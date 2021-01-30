using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The TeleportationPotion (0x49) packet.
    /// </summary>
    public class TeleportationPotion : TerrariaPacket
    {
        /// <summary>
        /// Gets or Sets the type - 0 = Teleportation Potion, 1 = Magic Conch, 2 = Demon Conch|
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportationPotion"/> class.
        /// </summary>
        public TeleportationPotion()
            : base((byte)PacketTypes.TeleportationPotion)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportationPotion"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public TeleportationPotion(BinaryReader br)
            : base(br)
        {
            this.Type = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[TeleportationPotion: Type = {Type}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(1);
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
                br.Write(Type);
            }
        }

        #endregion

    }
}
