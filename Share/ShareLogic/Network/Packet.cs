using Kosher.Sockets.Interface;
using Newtonsoft.Json;
using System.Text;

namespace ShareLogic.Network
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
        public Packet(ushort protocol, string body)
        {
            Protocol = protocol;
            Body = Encoding.UTF8.GetBytes(body);
        }

        public int GetLength()
        {
            return sizeof(ushort) + Body.Length;
        }
        public static Packet MakePacket<T>(ushort protocol, T packetData)
        {
            var packet = new Packet(protocol, JsonConvert.SerializeObject(packetData));
            return packet;
        }
    }
}
