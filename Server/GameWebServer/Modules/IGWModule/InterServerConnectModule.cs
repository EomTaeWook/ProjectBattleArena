using GameWebServer.Modules.IGWModule.Handler;
using GameWebServer.Modules.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Protocol.InterAndGWS;
using System.Reflection;

namespace GameWebServer.Modules.IGWModule
{
    public class InterServerClientModule : Singleton<InterServerClientModule>
    {
        private class InnerClient : BaseClient
        {
            public InnerClient(SessionCreator sessionCreator) : base(sessionCreator)
            {
            }

            protected override void OnConnected(Session session)
            {
                LogHelper.Info($"[GWS] connected inter server session : {session.Id}");
            }

            protected override void OnDisconnected(Session session)
            {
                LogHelper.Info($"[GWS] disconnected inter server session : {session.Id}");
                Task.Run(async () =>
                {
                    InterServerClientModule.Instance.Dispose();
                    await Task.Delay(1000);
                    InterServerClientModule.Instance.Connect();
                });
            }
        }
        private InnerClient _client;
        private int connectd = 0;
        public InterServerClientModule()
        {
        }
        public void Init()
        {
            InterServerFuncHandler.Initialization();
        }
        public void Connect()
        {
            if(connectd == 1)
            {
                return;
            }

            if(Interlocked.CompareExchange(ref connectd ,1 , 0) == 0)
            {
                try
                {
                    _client = new InnerClient(new SessionCreator(MakeSerializersFunc));
#if DEBUG
                    _client.Connect("13.125.232.85", 31000);
#else
                    _client.Connect("127.0.0.1", 31000);
#endif
                    return;
                }
                catch (Exception ex)
                {
                    connectd = 0;
                    LogHelper.Error(ex);
                }
            }
            if(connectd ==0)
            {
                Task.Run(async () =>
                {
                    LogHelper.Info($"thread : {Environment.CurrentManagedThreadId} | reconnect.. inter server");
                    await Task.Delay(5000);
                    Connect();
                });
            }
        }
        public void Start()
        {
            Task.Run(() =>
            {
                Connect();
            }).GetAwaiter().GetResult();
        }
        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref connectd, 0, 1) == 1)
            {
                _client = null;
            }
        }
        private Tuple<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>> MakeSerializersFunc()
        {
            InterServerFuncHandler handler = new InterServerFuncHandler();

            return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                new PacketSerializer(),
                new PacketDeserializer(handler),
                new List<ISessionComponent>() { handler }
                );
        }
    }
}
