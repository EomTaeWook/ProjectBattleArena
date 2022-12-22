﻿using BA.InterServer.ServerModule.Serializer;
using BA.Repository.Helper;
using BA.Repository;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using BA.InterServer.Manager;
using Protocol.InterAndGWS.ShareModels;

namespace BA.InterServer.ServerModule
{
    internal class InterServer : BaseServer
    {
        public InterServer(SessionCreator sessionCreator) : base(sessionCreator)
        {
        }

        protected override void OnAccepted(Session session)
        {
            LogHelper.Info($"[InterServer] acceptd session : {session.Id}");

            var packetData = new ChangedSecurityKey
            {
                PrivateKey = SchedulerSecurityManager.Instance.LatestPrivateKey
            };
            var packet = ServerModule.Packet.MakePacket(IGWSProtocol.GameWebServerInspection, packetData);
            session.Send(packet);
        }

        protected override void OnDisconnected(Session session)
        {
            LogHelper.Info($"[InterServer] disconnected : {session.Id}");
        }
    }
    internal class InterServerModule : Singleton<InterServerModule>
    {
        private SessionCreator creator;
        private InterServer _server;
        private bool isActive = false;
        public InterServerModule()
        {
            creator = new SessionCreator(MakeSerializersFunc);

            _server = new InterServer(creator);
        }
        public void Start()
        {
            Task.Run(async () =>
            {
                _server.Start(31000);
                LogHelper.Debug($"inter server start... port : 31000 ");
                isActive = true;
                while (isActive)
                {
                    await Task.Delay(33);
                }
            }).GetAwaiter().GetResult();
        }
        private Tuple<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>> MakeSerializersFunc()
        {
            return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                new PacketSerializer(),
                new PacketDeserializer(),
                new List<ISessionComponent>() {  }
                );
        }
        public void Broadcast(Packet packet)
        {
            foreach (var session in _server.GetAllSessions())
            {
                session.Send(packet);
            }
        }
    }
}