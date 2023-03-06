using BA.InterServer.Manager;
using BA.InterServer.ServerModule;
using Kosher.Sockets;
using Kosher.Sockets.Attribute;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic.Network;

namespace BA.InternalServer.Modules.Net
{
    public class GWSIProtocolHandler : ISessionComponent, IProtocolHandler<string>
    {
        public Session Session { get; private set; }
        public void Dispose()
        {
            InternalServerModule.Instance.RemoveGwsSession(Session);
        }

        public void SetSession(Session session)
        {
            Session = session;
        }
        [ProtocolName("RequestSecurity")]
        public void Process(RequestSecurity body)
        {
            if (body.ServerName == "GWS")
            {
                InternalServerModule.Instance.AddGwsSession(Session);
                var packetData = new ChangedSecurityKey
                {
                    PrivateKey = SchedulerSecurityManager.Instance.LatestPrivateKey
                };
                var packet = Packet.MakePacket((ushort)IGWSProtocol.ChangedSecurityKey, packetData);
                Session.Send(packet);
            }
        }

        public T DeserializeBody<T>(string body)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(body);
        }
    }
}
