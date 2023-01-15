using DataContainer.Generated;
using GameContents;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System;
using System.Collections.Generic;
using TemplateContainers;

namespace BA.GameServer.Game
{
    public class BattleResource
    {
        public long Id { get; private set; }
        public int RandomSeed { get; private set; }
        private readonly Battle _battle;
        public Battle GetBattle()
        {
            return _battle;
        }
        public BattleResource(long id,
            List<CharacterData> allies,
            List<CharacterData> enemies,
            Func<IBattleEventHandler> createEventHandlerFunc)
        {
            Id = id;
            RandomSeed = DateTime.Now.Ticks.GetHashCode();

            var allyUnits = new List<UnitInfo>();
            foreach(var item in allies)
            {
                var template = TemplateContainer<CharacterTemplate>.Find(item.TemplateId);
                var unitInfo = new UnitInfo()
                {
                    Atk = item.Atk,
                    CharacterName = item.CharacterName,
                    CharacterTemplate = template,
                    Con = item.Con,
                    Dex = item.Dex,
                    Level = LevelUpHelper.GetLevel(item.Exp),
                };

                var tempMap = new Dictionary<long, SkillData>();
                foreach(var skillData in item.SkillDatas)
                {
                    tempMap.Add(skillData.Id, skillData);
                }
                foreach(var mountingSkill in item.MountingSkillDatas)
                {
                    if(mountingSkill == -1)
                    {
                        continue;
                    }
                    var templateId = tempMap[mountingSkill].TemplateId;

                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(templateId);
                    unitInfo.MountingSkillDatas.Add(skillTemplate);
                }

                allyUnits.Add(unitInfo);
            }

            var enemyUnits = new List<UnitInfo>();
            foreach (var item in enemies)
            {
                var template = TemplateContainer<CharacterTemplate>.Find(item.TemplateId);
                var unitInfo = new UnitInfo()
                {
                    Atk = item.Atk,
                    CharacterName = item.CharacterName,
                    CharacterTemplate = template,
                    Con = item.Con,
                    Dex = item.Dex,
                    Level = LevelUpHelper.GetLevel(item.Exp),
                };
                var tempMap = new Dictionary<long, SkillData>();
                foreach (var skillData in item.SkillDatas)
                {
                    tempMap.Add(skillData.Id, skillData);
                }
                foreach (var mountingSkill in item.MountingSkillDatas)
                {
                    if (mountingSkill == -1)
                    {
                        continue;
                    }
                    var templateId = tempMap[mountingSkill].TemplateId;

                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(templateId);
                    unitInfo.MountingSkillDatas.Add(skillTemplate);
                }
                enemyUnits.Add(unitInfo);
            }

            _battle = new Battle(createEventHandlerFunc(), RandomSeed, allyUnits, enemyUnits);
        }
    }
}
