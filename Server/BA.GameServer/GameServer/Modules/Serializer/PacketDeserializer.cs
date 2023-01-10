using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets.Interface;
using System;
using System.Text;

namespace BA.GameServer.Modules.Serializer
{
    internal class PacketDeserializer : IPacketDeserializer
    {
        private static readonly int ProtocolSize = sizeof(ushort);
        readonly IStringFuncHandler _funcHandler;
        public PacketDeserializer(IStringFuncHandler funcHandler)
        {
            _funcHandler = funcHandler;
        }
        public const int LegnthSize = sizeof(int);
        public void Deserialize(Vector<byte> buffer)
        {
            var packetSizeBytes = buffer.Peek(LegnthSize);
            var length = BitConverter.ToInt32(packetSizeBytes);
            var bytes  = buffer.Read(length);

            short protocolValue = BitConverter.ToInt16(bytes);

            var body = Encoding.UTF8.GetString(bytes, ProtocolSize, bytes.Length - ProtocolSize);
            if (_funcHandler.CheckProtocol(protocolValue) == false)
            {
                LogHelper.Error($"not found callback - {protocolValue}");
                buffer.Clear();
                return;
            }
            _funcHandler.Execute(protocolValue, body);
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
