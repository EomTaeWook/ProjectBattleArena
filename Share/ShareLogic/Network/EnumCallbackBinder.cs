using System;
using System.Collections.Generic;
using System.Reflection;

namespace ShareLogic.Network
{
    public class EnumCallbackBinder<T, TKey, TBody> : ICallbackBinder<TKey, TBody> where TKey : Enum
    {
        private static readonly Dictionary<TKey, MethodInfo> _methodInfoMap = new Dictionary<TKey, MethodInfo>();
        public static void Initialization()
        {
            var enumType = typeof(TKey);
            foreach (var item in enumType.GetEnumNames())
            {
                var methodInfo = typeof(T).GetMethod(item);
                _methodInfoMap.Add((TKey)Enum.Parse(enumType, item), methodInfo);
            }
        }

        public void Execute(TKey protocol, TBody body)
        {
            _methodInfoMap[protocol].Invoke(this, new object[] { body });
        }

        public bool CheckProtocol(TKey protocol)
        {
            return _methodInfoMap.ContainsKey(protocol);
        }

        public virtual void Dispose()
        {
        }

        public EnumCallbackBinder()
        {
        }
    }
}
