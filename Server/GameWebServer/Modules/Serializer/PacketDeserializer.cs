using GameWebServer.Modules.IGWModule.Handler;
using Kosher.Collections;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using System.Text;

namespace GameWebServer.Modules.Serializer
{
    internal class PacketDeserializer : IPacketDeserializer
    {
        private const int ProtocolSize = sizeof(ushort);

        private InterServerFuncHandler _interServerFuncHandler;
        public PacketDeserializer(InterServerFuncHandler interServerFuncHandler)
        {
            _interServerFuncHandler = interServerFuncHandler;
        }
        public const int LegnthSize = sizeof(int);
        public void Deserialize(Vector<byte> buffer)
        {
            var packetSizeBytes = buffer.Read(LegnthSize);
            var length = BitConverter.ToInt32(packetSizeBytes);

            var bytes = buffer.Read(length);

            IGWSProtocol protocol = (IGWSProtocol)BitConverter.ToInt16(bytes);

            var body = Encoding.UTF8.GetString(bytes, ProtocolSize, bytes.Length - ProtocolSize);

            _interServerFuncHandler.Process(protocol, body);
        }

        public bool IsTakedCompletePacket(Vector<byte> buffer)
        {
            if (buffer.Count < LegnthSize)
            {
                return false;
            }
            var packetSizeBytes = buffer.Peek(LegnthSize);
            var length = BitConverter.ToInt32(packetSizeBytes);
            return buffer.Count >= length;
        }
    }
}
