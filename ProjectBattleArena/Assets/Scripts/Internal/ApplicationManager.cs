using Kosher.Framework;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ApplicationManager : Singleton<ApplicationManager>
    {
        private const string LocalUrl = @"http://localhost:10000";

        public string CurrentServerUrl { get; private set; }

        public void Init()
        {
            SetResolution(1080, 1920);

            CurrentServerUrl = LocalUrl;
        }
        private void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, true);
        }
    }
}
