namespace GameWebServer.Modules.IGWModule.Handler
{
    public class HandlerBinder<TKey, TBody>
    {
        private readonly Dictionary<TKey, Action<TBody>> _callbackMap = new Dictionary<TKey, Action<TBody>>();

        public delegate Action<TBody> OnCallback(TBody body);
        public void BindCallback(TKey protocol, Action<TBody> action)
        {
            _callbackMap.Add(protocol, action);
        }
        public void Execute(TKey protocol, TBody body)
        {
            _callbackMap[protocol].Invoke(body);
        }
        public void BindCallback(TKey protocol)
        {
            var method = this.GetType().GetMethod(protocol.ToString());
            if(method == null)
            {
                throw new MissingMethodException(protocol.ToString());
            }
            Action<TBody> action = (Action<TBody>)Delegate.CreateDelegate(typeof(Action<TBody>), this, method);
            _callbackMap.Add(protocol, action);
        }
        public bool CheckProtocol(TKey protocol)
        {
            return _callbackMap.ContainsKey(protocol);
        }
        public virtual void Dispose()
        {
            _callbackMap.Clear();
        }
    }
}
