using GameWebServer.Manager;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic;

namespace GameWebServer.Modules.IGWModule
{
    public class IGWSProtocolHandler : ISessionComponent, IProtocolHandler<string>
    {
        private Session _session;

        public void GameWebServerInspection(GameWebServerInspection packet)
        {
            if (ServiceManager.Instance.IsServerOn() != packet.ServerOn)
            {
                ServiceManager.Instance.SetServerState(packet.ServerOn);
            }
        }
        public void ChangedSecurityKey(ChangedSecurityKey packet)
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
        public T DeserializeBody<T>(string body)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(body);
        }
    }
}
