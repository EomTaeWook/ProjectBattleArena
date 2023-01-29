using BA.InterServer.Modules.Net.Handler;
using Kosher.Sockets.Interface;
using System.Text;

namespace BA.InterServer.ServerModule.Serializer
{
    internal class PacketDeserializer : IPacketDeserializer
    {
        private const int ProtocolSize = sizeof(ushort);
        readonly GWSIProtocolHandler _handler;
        public PacketDeserializer(GWSIProtocolHandler handler)
        {
            _handler = handler;
        }
        public const int LegnthSize = sizeof(int);
        public bool IsTakedCompletePacket(BinaryReader stream)
        {
            if (stream.BaseStream.Length < LegnthSize)
            {
                return false;
            }
            var length = stream.ReadInt32();
            stream.BaseStream.Seek(0, SeekOrigin.Begin);
            return stream.BaseStream.Length >= length;
        }

        public void Deserialize(BinaryReader stream)
        {
            var packetSize = stream.ReadInt32();

            var bytes = stream.ReadBytes(packetSize);

            int protocol = BitConverter.ToInt16(bytes);

            var body = Encoding.UTF8.GetString(bytes, ProtocolSize, bytes.Length - ProtocolSize);

            _handler.Process(protocol, body);
        }
    }
}
