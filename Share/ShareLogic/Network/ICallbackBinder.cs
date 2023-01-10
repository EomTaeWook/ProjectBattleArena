namespace ShareLogic.Network
{
    public interface ICallbackBinder<TKey, TBody>
    {
        void Execute(TKey protocol, TBody body);
        bool CheckProtocol(TKey protocol);
        void Dispose();
    }
}
