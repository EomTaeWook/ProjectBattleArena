using CLISystem.Attribude;
using CLISystem.Interface;
using System.Diagnostics;

namespace BA.InterServer.CLICommand
{
    [Cmd("close")]
    internal class Close : ICmdProcessor
    {
        public void Invoke(string[] args)
        {
            Process.GetCurrentProcess().Close();
        }

        public string Print()
        {
            return "현재 프로세스를 종료합니다.";
        }
    }
}
