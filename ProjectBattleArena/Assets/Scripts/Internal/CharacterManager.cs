using Kosher.Framework;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class CharacterManager : Singleton<CharacterManager>
    {
        public CharacterData SelectedCharacterData { get; private set; }

        public int CharacterCount { get => _characterDatas.Count; }

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

        public void Clear()
        {
            _characterDatas.Clear();
        }
        public void SetSelectedCharacterData(CharacterData characterData)
        {
            SelectedCharacterData = characterData;
        }

        public GameObject LoadCharcterResource()
        {
            var jobType = SelectedCharacterData.Job;
            var prefab = KosherUnityResourceManager.Instance.LoadResouce<GameObject>($"Prefabs/Character/{jobType}");

            var go = KosherUnityObjectPool.Instance.Pop(prefab);

            return go;
        }

    }
}
