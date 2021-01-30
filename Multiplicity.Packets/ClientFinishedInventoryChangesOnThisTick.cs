using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ClientFinishedInventoryChangesOnThisTick (0x8A) packet.
    /// <para>
    /// Sent by the client code in TrySyncingMyPlayer twice when a player moves an item around in their inventory. 
    /// Packet actually has no data. Total payload size is 2 packets per inventory item drag, 
    /// with 3 bytes each (2 for length, 1 for packet ID). This is a functionally useless packet.
    /// </para>
    /// </summary>
    public class ClientFinishedInventoryChangesOnThisTick : TerrariaPacket
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFinishedInventoryChangesOnThisTick"/> class.
        /// </summary>
        public ClientFinishedInventoryChangesOnThisTick()
            : base((byte)PacketTypes.ClientFinishedInventoryChangesOnThisTick)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFinishedInventoryChangesOnThisTick"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ClientFinishedInventoryChangesOnThisTick(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"[ClientFinishedInventoryChangesOnThisTick:]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(0);
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
            }
        }

        #endregion

    }
}
