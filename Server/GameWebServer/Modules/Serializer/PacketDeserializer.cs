using GameContents;
using GameWebServer.Modules.IGWModule.Handler;
using Kosher.Collections;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using System;
using System.Text;

namespace GameWebServer.Modules.Serializer
{
    internal class PacketDeserializer : IPacketDeserializer
    {
        private const int ProtocolSize = sizeof(ushort);

        private IGWSProtocolHandler _interServerFuncHandler;
        public PacketDeserializer(IGWSProtocolHandler interServerFuncHandler)
        {
            _interServerFuncHandler = interServerFuncHandler;
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

            _interServerFuncHandler.Process(protocol, body);
        }
    }
}
