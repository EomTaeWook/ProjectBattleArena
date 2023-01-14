using DataContainer.Generated;
using Kosher.Framework;
using Kosher.Unity;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TemplateContainers;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class CharacterManager : Singleton<CharacterManager>
    {
        public int Level { get; private set; }
        public CharacterData SelectedCharacterData { get; private set; }

        public int CharacterCount { get => _characterDatas.Count; }

        private List<CharacterData> _characterDatas = new List<CharacterData>();

        private Dictionary<long, SkillData> _skillContainer = new Dictionary<long, SkillData>();

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
        public ReadOnlyCollection<SkillData> GetSkillDatas()
        {
            return new ReadOnlyCollection<SkillData>(SelectedCharacterData.SkillDatas);
        }

        public void Clear()
        {
            _characterDatas.Clear();
        }
        public void SetSelectedCharacterData(CharacterData characterData)
        {
            SelectedCharacterData = characterData;
            _skillContainer.Clear();
            foreach(var item in characterData.SkillDatas)
            {
                _skillContainer.Add(item.Id, item);
            }

            Level = LevelUpHelper.GetLevel(characterData.Exp);
        }
        public SkillData GetSkillData(long id)
        {
            _skillContainer.TryGetValue(id, out SkillData skillData);

            return skillData;
        }
        public void AddSkillDatas(List<SkillData> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            SelectedCharacterData.SkillDatas.AddRange(items);
        }
    }
}
