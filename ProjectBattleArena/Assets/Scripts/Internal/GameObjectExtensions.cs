using Kosher.Unity;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal static class GameObjectExtensions
    {
        public static void Recycle(this GameObject gameObject)
        {
            if(object.ReferenceEquals(gameObject, null))
            {
                Debug.LogError($"{nameof(gameObject)} is null");
                return;
            }
            gameObject.transform.SetParent(KosherUnityObjectPool.Instance.transform, false);
            gameObject.SetActive(false);
            KosherUnityObjectPool.Instance.Push(gameObject);
        }
    }
}
