using Kosher.Framework;
using Protocol.GameWebServerAndClient;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ApplicationManager : Singleton<ApplicationManager>
    {
        private const string LocalUrl = @"http://localhost:10000";
        private const string DevUrl = @"http://localhost:10000";

        public string CurrentServerUrl { get; private set; }
        private Vector2Int _resolution = new Vector2Int(768, 1280);
        public void Init()
        {
            SetResolution(_resolution.x, _resolution.y);

            CurrentServerUrl = LocalUrl;
        }
        private void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, true);
        }
        public Vector2Int GetResolution()
        {
            return _resolution;
        }
    }
}
