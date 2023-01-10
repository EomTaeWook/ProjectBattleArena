using BA.GameServer.Modules.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using ShareLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BA.GameServer.Modules.Stun
{
    public class StunModule : Singleton<StunModule>
    {
        private class InnerServer : BaseServer
        {
            public InnerServer(SessionCreator sessionCreator) : base(sessionCreator)
            {
            }

            protected override void OnAccepted(Session session)
            {

            }

            protected override void OnDisconnected(Session session)
            {

            }
        }
        private bool isActive = false;
        private InnerServer _server;

        public StunModule()
        {
            SessionCreator creator = new SessionCreator(MakeSerializersFunc);
            _server = new InnerServer(creator);
        }
        public void Start()
        {
            Task.Run(async () =>
            {
                _server.Start("", ConstHelper.StunServerPort, ProtocolType.Udp);
                LogHelper.Debug($"stun server start... port : {ConstHelper.StunServerPort}");
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
                new DefaultPacketDeserializer(),
                new List<ISessionComponent>() { }
                );
        }
    }
}
