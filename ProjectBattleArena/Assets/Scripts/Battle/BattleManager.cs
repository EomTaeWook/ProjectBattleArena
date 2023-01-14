using Assets.Scripts.Internal;
using DataContainer.Generated;
using GameContents;
using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;
using System.Collections.Generic;
using TemplateContainers;

public class BattleManager : Singleton<BattleManager>
{
    public Battle MakeBattle(int seed,
        List<CharacterData> allies,
        List<CharacterData> enemies
        )
    {
        var alliesDatas = new List<UnitInfo>();

        foreach(var item in allies)
        {
            var charcterTemplate = TemplateContainer<CharacterTemplate>.Find(item.TemplateId);

            var unitInfo = new UnitInfo()
            {
                Atk = item.Atk,
                CharacterName = item.CharacterName,
                CharacterTemplate = charcterTemplate,
                Con = item.Con,
                Dex = item.Dex,
                Level = LevelUpHelper.GetLevel(item.Exp),
            };
            foreach(var id in item.MountingSkillDatas)
            {
                var skillData = CharacterManager.Instance.GetSkillData(id);
                if(skillData != null)
                {
                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(skillData.TemplateId);
                    unitInfo.EquippedSkillDatas.Add(skillTemplate);
                }
            }
            alliesDatas.Add(unitInfo);
        }

        var enemiesDatas = new List<UnitInfo>();

        foreach (var item in enemies)
        {
            var charcterTemplate = TemplateContainer<CharacterTemplate>.Find(item.TemplateId);

            var unitInfo = new UnitInfo()
            {
                Atk = item.Atk,
                CharacterName = item.CharacterName,
                CharacterTemplate = charcterTemplate,
                Con = item.Con,
                Dex = item.Dex,
                Level = LevelUpHelper.GetLevel(item.Exp),
            };
            foreach (var id in item.MountingSkillDatas)
            {
                var skillData = CharacterManager.Instance.GetSkillData(id);
                if (skillData != null)
                {
                    var skillTemplate = TemplateContainer<SkillsTemplate>.Find(skillData.TemplateId);
                    unitInfo.EquippedSkillDatas.Add(skillTemplate);
                }
            }
            enemiesDatas.Add(unitInfo);
        }

        BattleEventHandler eventHandler = new BattleEventHandler();
        var battle = new Battle(eventHandler,
            seed,
            alliesDatas,
            enemiesDatas
            );
        return battle;
    }

    public Battle MakeBattle(int seed,
        CharacterData ally,
        CharacterData enemy)
    {
        var allies = new List<CharacterData>()
        {
            ally
        };
        var enemies = new List<CharacterData>()
        {
            enemy
        };

        return MakeBattle(seed, allies, enemies);
    }
}