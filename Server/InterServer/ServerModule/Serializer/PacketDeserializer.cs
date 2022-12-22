using Kosher.Collections;
using Kosher.Sockets.Interface;

namespace BA.InterServer.ServerModule.Serializer
{
    internal class PacketDeserializer : IPacketDeserializer
    {
        public PacketDeserializer()
        {
            
        }
        public const int LegnthSize = sizeof(int);
        public void Deserialize(Vector<byte> buffer)
        {
        }

        public bool IsTakedCompletePacket(Vector<byte> buffer)
        {
            if(buffer.Count < LegnthSize)
            {
                return false;
            }
            var packetSizeBytes = buffer.Peek(LegnthSize);
            var length = BitConverter.ToInt32(packetSizeBytes);
            return buffer.Count >= length;
        }
    }
}
