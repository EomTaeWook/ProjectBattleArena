using BA.InterServer.Manager;
using CLISystem.Attribude;
using CLISystem.Interface;

namespace BA.InterServer.CLICommand
{
    [Cmd("resetkey")]
    internal class ResetKey : ICmdProcessor
    {
        public async void Invoke(string[] args)
        {
            var _ = await SchedulerSecurityManager.Instance.CreateKey();
        }

        public string Print()
        {
            return "암호키를 재생성합니다.";
        }
    }
}
