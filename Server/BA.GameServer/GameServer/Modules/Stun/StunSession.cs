using Kosher.Sockets;

namespace BA.GameServer.Modules.Stun
{
    public class StunSession
    {
        public Session Client { get; private set; }
        public StunSession(Session session)
        {
            Client = session;
        }
    }
}
