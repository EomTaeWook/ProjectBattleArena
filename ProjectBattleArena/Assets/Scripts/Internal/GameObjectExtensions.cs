using Kosher.Log;
using Kosher.Unity;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal static class GameObjectExtensions
    {
        public static bool IsNull(this GameObject gameObject)
        {
            return object.ReferenceEquals(gameObject, null);
        }
        public static void Recycle(this GameObject gameObject)
        {
            if(object.ReferenceEquals(gameObject, null))
            {
                LogHelper.Info($"{nameof(gameObject)} is null");
                return;
            }
            gameObject.transform.SetParent(KosherUnityObjectPool.Instance.transform, false);
            gameObject.SetActive(false);
            KosherUnityObjectPool.Instance.Push(gameObject);
        }
    }
}
