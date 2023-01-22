using GameWebServer.Manager;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic;
using ShareLogic.Network;
using System.Text.Json;

namespace GameWebServer.Modules.IGWModule.Handler
{
    public partial class IGWSProtocolHandler : EnumCallbackBinder<IGWSProtocolHandler, IGWSProtocol, string>, ISessionComponent
    {
        private Session _session;

        public void Process(IGWSProtocol protocol, string body)
        {
            if(CheckProtocol(protocol) == false)
            {
                LogHelper.Error($"not found callback - {protocol}");
                return;
            }
            Execute(protocol, body);
        }
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
        public T DeserializeBody<T>(string body)
        {
            return JsonSerializer.Deserialize<T>(body);
        }
        public override void Dispose()
        {
            base.Dispose();
        }

        public void SetSession(Session session)
        {
            _session = session;
        }
    }
}
