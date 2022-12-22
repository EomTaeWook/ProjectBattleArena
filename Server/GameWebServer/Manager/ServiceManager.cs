using Kosher.Framework;

namespace GameWebServer.Manager
{
    public class ServiceManager : Singleton<ServiceManager>
    {
        private bool _serverOn = true;

        public bool IsServerOn()
        {
            return _serverOn;
        }

        public void SetServerState(bool isOn)
        {
            _serverOn = isOn;
        }
    }
}
