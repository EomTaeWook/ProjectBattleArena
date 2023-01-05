using System;
using System.Collections.Generic;

namespace ShareLogic.Network
{
    public class EnumCallbackBinder<T, TKey, TBody> : CallbackBinder<TKey, TBody>  where TKey : Enum where T : CallbackBinder<TKey, TBody>
    {
        static Dictionary<TKey, Action<TBody>> _methodCaches = new Dictionary<TKey, Action<TBody>>();
        public static void Initialization()
        {
            foreach (var item in typeof(TKey).GetEnumNames())
            {
                var methodInfo = typeof(T).GetMethod(item);
                Action<TBody> action = (Action<TBody>)Delegate.CreateDelegate(typeof(Action<TBody>), methodInfo);
                _methodCaches.Add((TKey)Enum.Parse(typeof(TKey), item), action);
            }
        }
        public EnumCallbackBinder()
        {
            foreach(var item in _methodCaches)
            {
                Bind(item.Key, item.Value);
            }
        }
    }
}
