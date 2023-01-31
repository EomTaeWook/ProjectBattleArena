using GameWebServer.Manager;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic;

namespace GameWebServer.Modules.IGWModule.Handler
{
    public partial class IGWSProtocolHandler : ISessionComponent
    {
        private Session _session;
       
        public void Process(GameWebServerInspection packet)
        {
            if (ServiceManager.Instance.IsServerOn() != packet.ServerOn)
            {
                ServiceManager.Instance.SetServerState(packet.ServerOn);
            }
        }
        public void Process(ChangedSecurityKey packet)
        {
            Cryptogram.SetPrivateKey(packet.PrivateKey);
        }
        public void SetSession(Session session)
        {
            _session = session;
        }

        public void Dispose()
        {
            
        }
    }
}
