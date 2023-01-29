using Kosher.Log;

namespace GameWebServer.Modules.IGWModule.Handler
{
    public interface IIGWSProtocolHandler
    {
        public T DeserializeBody<T>(string body);
    }
    public partial class IGWSProtocolHandler : IIGWSProtocolHandler
    {
        private static Action<IGWSProtocolHandler, string>[] _handlers;
        public static void Init()
        {
            _handlers = new Action<IGWSProtocolHandler, string>[2];
            _handlers[0] = (t, body) => t.ProcessGameWebServerInspection(body);
            _handlers[1] = (t, body) => t.ProcessChangedSecurityKey(body);
        }
        public static bool CheckProtocol(int protocol)
        {
            if(protocol < 0 && protocol >= _handlers.Length)
            {
                return false;
            }
            return true;
        }
        public void Process(int protocol, string body)
        {
            _handlers[protocol](this, body);
        }
        protected void ProcessGameWebServerInspection(string body)
        {
            if(body == null)
            {
                LogHelper.Error("body is null");
                return;
            }
            var packet = DeserializeBody<Protocol.InterAndGWS.ShareModels.GameWebServerInspection>(body);
            Process(packet);
        }
        protected void ProcessChangedSecurityKey(string body)
        {
            if(body == null)
            {
                LogHelper.Error("body is null");
                return;
            }
            var packet = DeserializeBody<Protocol.InterAndGWS.ShareModels.ChangedSecurityKey>(body);
            Process(packet);
        }
    }
}
