﻿using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets.Interface;
using ShareLogic.Network;

namespace BA.InterServer.ServerModule.Serializer
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
            LogHelper.Debug($"send : {sendPacket.Protocol}");

            return new ArraySegment<byte>(buffer.ToArray());
        }
    }
}
