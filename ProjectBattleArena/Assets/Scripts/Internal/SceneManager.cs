using Assets.Scripts.Models;
using Kosher.Framework;
using Kosher.Unity;
using System;

namespace Assets.Scripts.Internal
{
    internal class SceneManager : Singleton<SceneManager>
    {
        public void LoadScene(SceneType sceneType, Action onCompleteCallback = null)
        {
            KosherUnitySceneManager.Instance.LoadSceneAsync(sceneType.ToString(), onCompleteCallback);
        }
    }
}
