using BA.InterServer.ServerModule;
using CLISystem.Attribude;
using CLISystem.Interface;
using Protocol.InterAndGWS;
using Protocol.InterAndGWS.ShareModels;
using ShareLogic.Network;

namespace BA.InterServer.CLICommand
{
    [Cmd("gws")]
    internal class GwsOnOff : ICmdProcessor
    {
        public void Invoke(string[] args)
        {
            var packetData = new GameWebServerInspection();

            if (args[0] == "on")
            {
                packetData.ServerOn = true;
            }
            else
            {
                packetData.ServerOn = false;
            }
            var packet = Packet.MakePacket((ushort)IGWSProtocol.GameWebServerInspection, packetData);
            InternalServerModule.Instance.Broadcast(packet);
        }
        public string Print()
        {
            return "GameWebServer 점검 On, Off 명령어 입니다.";
        }
    }
}
