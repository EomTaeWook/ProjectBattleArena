using Kosher.Sockets.Interface;

namespace BA.GameServer.Modules
{
    public class Packet : IPacket
    {
        public ushort Protocol { get; set; }
        public byte[] Body { get; set; }

        public Packet(ushort protocol, byte[] body)
        {
            Protocol = protocol;
            Body = body;
        }

        public int GetLength()
        {
            return sizeof(ushort) + Body.Length;
        }

    }
}
