namespace BA.GameServer.Modules
{
    public interface IStringFuncHandler
    {
        public bool CheckProtocol(short protocol);

        public void Execute(short protocol, string body);
    }
}
