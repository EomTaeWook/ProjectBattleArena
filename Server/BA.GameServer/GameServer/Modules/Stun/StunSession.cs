using Kosher.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
