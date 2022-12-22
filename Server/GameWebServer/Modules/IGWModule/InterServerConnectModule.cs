﻿using GameWebServer.Modules.IGWModule.Handler;
using GameWebServer.Modules.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Microsoft.AspNetCore.Hosting.Server;

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
        public void Connect()
        {
            if(_client != null)
            {
                return;
            }

            if(Interlocked.CompareExchange(ref connectd ,1 , 0) == 0)
            {
                try
                {
                    _client = new InnerClient(new SessionCreator(MakeSerializersFunc));
                    _client.Connect("127.0.0.1", 31000);
                    return;
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                }
            }
            Task.Run(async () =>
            {
                LogHelper.Info($"reconnect.. inter server");
                await Task.Delay(5000);
                Connect();
            });
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
