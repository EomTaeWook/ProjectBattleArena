using BA.GameServer.Modules.Game.Models;
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
        private readonly BattleEventHandler _battleEventHandler = new BattleEventHandler();
        private readonly Battle _battle;
        private Player Player { get; set; }
        public BattleResource(long id,
            List<CharacterData> allies,
            List<CharacterData> enemies)
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
                foreach(var skill in item.EquippedSkillDatas)
                {
                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(skill.SkillTemplate);

                    unitInfo.EquippedSkillDatas.Add(skillTemplate);
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
                foreach (var skill in item.EquippedSkillDatas)
                {
                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(skill.SkillTemplate);

                    unitInfo.EquippedSkillDatas.Add(skillTemplate);
                }
                enemyUnits.Add(unitInfo);
            }

            _battle = new Battle(_battleEventHandler, RandomSeed, allyUnits, enemyUnits);
        }
        public void ProcessBattle()
        {
            
        }
    }
}
