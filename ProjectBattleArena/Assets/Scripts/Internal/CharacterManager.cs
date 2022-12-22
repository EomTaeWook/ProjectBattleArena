using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModel;
using System.Collections.Generic;

namespace Assets.Scripts.Internal
{
    internal class CharacterManager : Singleton<CharacterManager>
    {
        private List<CharacterData> _characterDatas = new List<CharacterData>();
        public void Init(List<CharacterData> characterDatas)
        {
            _characterDatas.AddRange(characterDatas);
        }
        public int CharacterCount { get => _characterDatas.Count; }
        public void Clear()
        {
            _characterDatas.Clear();
        }
    }
}
