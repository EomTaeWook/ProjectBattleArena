using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets.Interface;
using ShareLogic.Network;

namespace BA.GameServer.Modules.Serializer
{
    internal class PacketSerializer : IPacketSerializer
    {
        public Vector<byte> MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var packetSize = sendPacket.GetLength();

            var buffer = new Vector<byte>();

            buffer.AddRange(BitConverter.GetBytes(packetSize));

            buffer.AddRange(BitConverter.GetBytes(sendPacket.Protocol));

            buffer.AddRange(sendPacket.Body);
            LogHelper.Debug($"send : {sendPacket.Protocol}");
            return buffer;
        }
    }
}
