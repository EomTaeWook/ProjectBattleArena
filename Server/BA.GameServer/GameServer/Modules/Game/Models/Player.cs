using BA.GameServer.Game;
using Kosher.Sockets;
using Protocol.GSC;
using ShareLogic.Network;

namespace BA.GameServer.Modules.Game.Models
{
    public class Player
    {
        public Session Session { get; private set; }

        public string Nickname { get; private set; }

         public BattleResource BattleResource { get; private set; }

        public Player(Session session, string nickname)
        {
            this.Session = session;
            this.Nickname = nickname;
        }
        public void SetBattleResource(BattleResource battleResource)
        {
            BattleResource = battleResource;
        }

        public void Send(Packet packet)
        {
            if(this.Session.IsDispose() == true)
            {
                return;
            }
            this.Session.Send(packet);
        }
        public void Send<T>(GSCProtocol protocol, T packetData)
        {
            if (this.Session.IsDispose() == true)
            {
                return;
            }
            Session.Send(Packet.MakePacket((ushort)protocol,
                    packetData));
        }
    }
}
