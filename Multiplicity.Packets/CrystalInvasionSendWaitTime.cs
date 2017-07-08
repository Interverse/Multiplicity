using System;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CrystalInvasionSendWaitTime (0x74) packet.
    /// </summary>
    public class CrystalInvasionSendWaitTime : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the TimeUntilNextWave - 1800 (30s) between waves, 30 (5s) when starting|
        /// </summary>
        public int TimeUntilNextWave { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrystalInvasionSendWaitTime"/> class.
        /// </summary>
        public CrystalInvasionSendWaitTime()
            : base((byte)PacketTypes.CrystalInvasionSendWaitTime)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrystalInvasionSendWaitTime"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public CrystalInvasionSendWaitTime(BinaryReader br)
            : base(br)
        {
            this.TimeUntilNextWave = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[CrystalInvasionSendWaitTime: TimeUntilNextWave = {TimeUntilNextWave}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(4);
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
                br.Write(TimeUntilNextWave);
            }
        }

        #endregion

    }
}
