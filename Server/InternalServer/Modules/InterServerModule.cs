using BA.InternalServer.Modules.Net;
using BA.InterServer.ServerModule.Serializer;
using Kosher.Framework;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
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
            LogHelper.Info($"[InternalServer] acceptd session : {session.Id}");

            //var packetData = new ChangedSecurityKey
            //{
            //    PrivateKey = SchedulerSecurityManager.Instance.LatestPrivateKey
            //};
            //var packet = Packet.MakePacket((ushort)IGWSProtocol.ChangedSecurityKey, packetData);
            //session.Send(packet);
        }

        protected override void OnDisconnected(Session session)
        {
            LogHelper.Info($"[InternalServer] disconnected : {session.Id}");
        }
    }
    internal class InternalServerModule : Singleton<InternalServerModule>
    {
        private readonly InterServer _server;
        private readonly HashSet<Session> _gwsSession = new HashSet<Session>();
        private bool _isActive = false;
        public InternalServerModule()
        {
            _server = new InterServer(new SessionCreator(MakeSerializersFunc));
        }
        public void Start()
        {
            Task.Run(async () =>
            {
                _server.Start(ConstHelper.InternalServerPort);
                LogHelper.Debug($"inter server start... port : {ConstHelper.InternalServerPort}");
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
