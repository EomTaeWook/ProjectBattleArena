using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using System.Text;
using System.Text.Json;

namespace BA.InterServer.ServerModule
{
    internal class Packet : IPacket
    {
        public IGWSProtocol Protocol { get; private set; }
        public byte[] Body { get; private set; }
        public Packet(IGWSProtocol protocol, string body)
        {
            Protocol = protocol;
            Body = Encoding.UTF8.GetBytes(body);
        }
        public Packet(IGWSProtocol protocol, byte[] body)
        {
            Protocol = protocol;
            Body = body;
        }
        public int GetLength()
        {
            return sizeof(ushort) + Body.Length;
        }

        public static Packet MakePacket<T>(IGWSProtocol protocol, T packetData)
        {
            var packet = new Packet(protocol, JsonSerializer.Serialize<T>(packetData));

            return packet;
        }
    }
}
