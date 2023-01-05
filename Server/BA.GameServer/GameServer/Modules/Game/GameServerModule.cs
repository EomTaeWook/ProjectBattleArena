using BA.GameServer.Modules.Game.Handler;
using BA.GameServer.Modules.Serializer;
using Kosher.Collections;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using ShareLogic;

namespace BA.GameServer.Modules.Game
{
    internal class GameServerModule : Singleton<GameServerModule>
    {
        private class InnerServer : BaseServer
        {
            private SynchronizedHashSet<Session> _sessions = new SynchronizedHashSet<Session>();
            public InnerServer(SessionCreator sessionCreator) : base(sessionCreator)
            {
            }

            protected override void OnAccepted(Session session)
            {
                _sessions.Add(session);
            }

            protected override void OnDisconnected(Session session)
            {
                _sessions.Remove(session);
            }
        }
        private bool isActive = false;
        private readonly InnerServer _server;
        public GameServerModule()
        {
            SessionCreator creator = new SessionCreator(MakeSerializersFunc);
            _server = new InnerServer(creator);
        }
        public static void Init()
        {
            GSCFuncHandler.Initialization();
        }
        public void Start()
        {
            Task.Run(async () =>
            {
                _server.Start("", ConstHelper.GameServerPort, ProtocolType.Tcp);
                LogHelper.Debug($"game server start... port : {ConstHelper.GameServerPort} ");
                isActive = true;
                while (isActive)
                {
                    await Task.Delay(33);
                }
            }).GetAwaiter().GetResult();
        }
        private Tuple<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>> MakeSerializersFunc()
        {
            var handler = new GSCFuncHandler();

            return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                new PacketSerializer(),
                new PacketDeserializer(
                    handler
                    ),
                new List<ISessionComponent>() { handler });
        }
    }
}
