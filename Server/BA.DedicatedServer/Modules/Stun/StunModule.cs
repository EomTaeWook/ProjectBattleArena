using BA.DedicatedServer.Modules.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;

namespace BA.DedicatedServer.Modules.Stun
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
                _server.Start("", 31100, ProtocolType.Udp);
                LogHelper.Debug($"stun server start... port : 31100 ");
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
                new List<ISessionComponent>() { }
                );
        }
    }
}
