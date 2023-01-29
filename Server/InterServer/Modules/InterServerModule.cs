using BA.InterServer.Manager;
using BA.InterServer.Modules.Net.Handler;
using BA.InterServer.ServerModule.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic;
using ShareLogic.Network;

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

            //var packetData = new ChangedSecurityKey
            //{
            //    PrivateKey = SchedulerSecurityManager.Instance.LatestPrivateKey
            //};
            //var packet = Packet.MakePacket((ushort)IGWSProtocol.ChangedSecurityKey, packetData);
            //session.Send(packet);
        }

        protected override void OnDisconnected(Session session)
        {
            LogHelper.Info($"[InterServer] disconnected : {session.Id}");
        }
    }
    internal class InterServerModule : Singleton<InterServerModule>
    {
        private readonly InterServer _server;
        private HashSet<Session> _gwsSession = new HashSet<Session>();
        private bool _isActive = false;
        public InterServerModule()
        {
            _server = new InterServer(new SessionCreator(MakeSerializersFunc));
        }
        public void Start()
        {
            Task.Run(async () =>
            {
                _server.Start(ConstHelper.InterServerPort);
                LogHelper.Debug($"inter server start... port : {ConstHelper.InterServerPort}");
                _isActive = true;
                while (_isActive)
                {
                    await Task.Delay(33);
                }
            }).GetAwaiter().GetResult();
        }
        private Tuple<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>> MakeSerializersFunc()
        {
            var handler = new GWSIProtocolHandler();

            return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(
                new PacketSerializer(),
                new PacketDeserializer(handler),
                new List<ISessionComponent>() { handler }
                );
        }
        public void AddGwsSession(Session session)
        {
            _gwsSession.Add(session);
        }
        public void RemoveGwsSession(Session session)
        {
            _gwsSession.Remove(session);
        }
        public void Broadcast(Packet packet)
        {
            foreach (var session in _gwsSession)
            {
                session.Send(packet);
            }
        }
    }
}
