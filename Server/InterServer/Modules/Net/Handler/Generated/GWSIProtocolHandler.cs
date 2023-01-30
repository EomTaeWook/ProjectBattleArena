using Kosher.Log;

namespace BA.InterServer.Modules.Net.Handler
{
    public partial class GWSIProtocolHandler
    {
        private static Action<GWSIProtocolHandler, string>[] _handlers;
        public static void Init()
        {
            _handlers = new Action<GWSIProtocolHandler, string>[1];
            _handlers[0] = (t, body) => t.ProcessRequestSecurity(body);
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
        protected void ProcessRequestSecurity(string body)
        {
            if(body == null)
            {
                LogHelper.Error("body is null");
                return;
            }
            var packet = DeserializeBody<Protocol.InterAndGWS.ShareModels.RequestSecurity>(body);
            Process(packet);
        }
        protected T DeserializeBody<T>(string body)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(body);
        }
    }
}
