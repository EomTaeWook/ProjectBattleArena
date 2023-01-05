using Kosher.Collections;
using Kosher.Sockets.Interface;

namespace BA.GameServer.Modules.Serializer
{
    internal class DefaultPacketDeserializer : IPacketDeserializer
    {
        public const int LegnthSize = sizeof(int);
        public void Deserialize(Vector<byte> buffer)
        {
            buffer.Clear();
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
