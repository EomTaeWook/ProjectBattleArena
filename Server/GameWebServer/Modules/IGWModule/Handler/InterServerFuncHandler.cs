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
    public class InterServerFuncHandler : EnumCallbackBinder<InterServerFuncHandler, IGWSProtocol, string>, ISessionComponent
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
        public void GameWebServerInspection(string body)
        {
            var packetData = JsonSerializer.Deserialize<GameWebServerInspection>(body);
            if(ServiceManager.Instance.IsServerOn() != packetData.ServerOn)
            {
                ServiceManager.Instance.SetServerState(packetData.ServerOn);
            }
        }
        public void ChangedSecurityKey(string body)
        {
            var packetData = JsonSerializer.Deserialize<ChangedSecurityKey>(body);

            Cryptogram.SetPrivateKey(packetData.PrivateKey);
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
