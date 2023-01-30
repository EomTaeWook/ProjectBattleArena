using BA.InterServer.Manager;
using BA.InterServer.ServerModule;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic.Network;
using System.Text.Json;

namespace BA.InterServer.Modules.Net.Handler
{
    public partial class GWSIProtocolHandler : ISessionComponent
    {
        public Session Session { get; private set; }
        public void Dispose()
        {
            InterServerModule.Instance.RemoveGwsSession(Session);
        }

        public void SetSession(Session session)
        {
            Session = session;
        }
        public void Process(RequestSecurity body)
        {
            if(body.ServerName == "GWS")
            {
                InterServerModule.Instance.AddGwsSession(Session);
                var packetData = new ChangedSecurityKey
                {
                    PrivateKey = SchedulerSecurityManager.Instance.LatestPrivateKey
                };
                var packet = Packet.MakePacket((ushort)IGWSProtocol.ChangedSecurityKey, packetData);
                Session.Send(packet);
            }
            
        }
    }
}
