using System;
using System.Collections.Generic;

namespace ShareLogic.Network
{
    public class CallbackBinder<TKey, TBody> : ICallbackBinder<TKey, TBody>
    {
        private readonly Dictionary<TKey, Action<TBody>> _callbackMap = new Dictionary<TKey, Action<TBody>>();

        public delegate Action<TBody> OnCallback(TBody body);

        public void Bind(TKey protocol, Action<TBody> action)
        {
            _callbackMap.Add(protocol, action);
        }
        public void Execute(TKey protocol, TBody body)
        {
            _callbackMap[protocol].Invoke(body);
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
