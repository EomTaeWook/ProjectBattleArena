using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets.Scripts.Internal
{
    internal class CharacterManager : Singleton<CharacterManager>
    {
        private List<CharacterData> _characterDatas = new List<CharacterData>();
        public void Init(List<CharacterData> characterDatas)
        {
            _characterDatas.AddRange(characterDatas);
        }
        public void Add(CharacterData characterData)
        {
            _characterDatas.Add(characterData);
        }
        public ReadOnlyCollection<CharacterData> GetCharacterDatas()
        {
            return new ReadOnlyCollection<CharacterData>(_characterDatas);
        }
        public int CharacterCount { get => _characterDatas.Count; }
        public void Clear()
        {
            _characterDatas.Clear();
        }
    }
}
