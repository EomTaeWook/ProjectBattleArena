using Kosher.Collections;
using Kosher.Sockets.Interface;

namespace GameWebServer.Modules.Serializer
{
    internal class PacketSerializer : IPacketSerializer
    {
        ArraySegment<byte> IPacketSerializer.MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var packetSize = sendPacket.GetLength();

            var buffer = new Vector<byte>();

            buffer.AddRange(BitConverter.GetBytes(packetSize));

            buffer.AddRange(BitConverter.GetBytes((ushort)sendPacket.Protocol));

            buffer.AddRange(sendPacket.Body);
            return buffer.ToArray();
        }
    }
}
