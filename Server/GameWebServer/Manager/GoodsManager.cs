using DataContainer;
using DataContainer.Generated;
using GameWebServer.Extensions;
using Kosher.Framework;
using Kosher.Log;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic.Random;
using TemplateContainers;

namespace GameWebServer.Manager
{
    public class GoodsManager : Singleton<GoodsManager>
    {
        private readonly List<ConstantTemplate> _gachaProp = new List<ConstantTemplate>();
        private readonly Dictionary<CharacterTemplate, List<SkillsGroupTemplate>> _skillsTemplateFromCharacter = new Dictionary<CharacterTemplate, List<SkillsGroupTemplate>>();

        public GoodsManager()
        {
            foreach(var template in TemplateContainer<SkillsGroupTemplate>.Values)
            {
                if (template.IsBaseAttack == true)
                {
                    continue;
                }

                if (_skillsTemplateFromCharacter.ContainsKey(template.UseCharacterRef) == false)
                {
                    _skillsTemplateFromCharacter.Add(template.UseCharacterRef, new List<SkillsGroupTemplate>());
                }
                _skillsTemplateFromCharacter[template.UseCharacterRef].Add(template);
            }

            _gachaProp.Add(TemplateContainer<ConstantTemplate>.Find("GachaNormal"));
            _gachaProp.Add(TemplateContainer<ConstantTemplate>.Find("GachaRare"));
            _gachaProp.Add(TemplateContainer<ConstantTemplate>.Find("GachaEpic"));
            _gachaProp.Add(TemplateContainer<ConstantTemplate>.Find("GachaLegendary"));
            _gachaProp.Add(TemplateContainer<ConstantTemplate>.Find("GachaMythic"));
        }

        public async Task<RewardData> PurchaseGoodsAsync(string account,
            CharacterTemplate characterTemplate,
            GoodsTemplate goodsTemplate,
            bool validateAsset = true)
        {
            var reward = new RewardData();

            if(validateAsset == true)
            {
                var preAssetCount = await AssetManager.Instance.LoadAssetAsync(account, goodsTemplate.PreAsset.AssetType);

                if (preAssetCount == null)
                {
                    LogHelper.Error($"{account} : failed to load asset - {goodsTemplate.Id}");
                    return null;
                }
                var currentCount = preAssetCount.Item1;

                AssetType consumeAssetType = goodsTemplate.PreAsset.AssetType;

                var consumeAssetCount = goodsTemplate.PreAsset.ConsumeAssetCount;

                if (currentCount < consumeAssetCount)
                {
                    var loadAssetCount = await AssetManager.Instance.LoadAssetAsync(account, goodsTemplate.Asset.AssetType);

                    if (loadAssetCount == null)
                    {
                        LogHelper.Error($"{account} : failed to load asset - {goodsTemplate.Id}");
                        return null;
                    }

                    currentCount = loadAssetCount.Item1;

                    consumeAssetCount = goodsTemplate.Asset.ConsumeAssetCount;

                    consumeAssetType = goodsTemplate.Asset.AssetType;
                }

                if (currentCount < consumeAssetCount)
                {
                    LogHelper.Error($"{account} : not enough asset - {goodsTemplate.Id}");
                    return null;
                }

                var modify = await AssetManager.Instance.ModifyAssetAsync(account,
                    consumeAssetType,
                    currentCount,
                    currentCount - consumeAssetCount,
                    ChangedAssetReason.PurchasedGoods);

                if (modify == false)
                {
                    LogHelper.Error($"{account} : failed to modify asset - {goodsTemplate.Id}");
                    return null;
                }

                reward.ModifyAsset(consumeAssetType, -consumeAssetCount);
            }

            RandomGenerator randomGenerator = new RandomGenerator();

            var get = GetReward(randomGenerator,
                           characterTemplate,
                           goodsTemplate.GoodsCategory,
                           goodsTemplate.Count);

            reward.Add(get);

            return reward;
        }
        private RewardData GetReward(RandomGenerator randomGenerator,
            CharacterTemplate characterTemplate,
            GoodsCategory category,
            int count)
        {
            var reward = new RewardData();
            if (category == GoodsCategory.GachaSkill)
            {
                for(int i=0; i< count; ++i)
                {
                    var templateId = GachaSkill(characterTemplate, randomGenerator);
                    reward.AcquiredSkills.Add(new SkillData() 
                    {
                        TemplateId = templateId,
                    });
                }
            }
            else if (category == GoodsCategory.Cash)
            {
                reward.CashDiff += count;
            }
            return reward;
        }

        public int GachaSkill(CharacterTemplate characterTemplate, RandomGenerator randomGenerator)
        {
            var groupTemplates = _skillsTemplateFromCharacter[characterTemplate];

            var index = randomGenerator.Next(groupTemplates.Count - 1);

            var groupTemplate = groupTemplates[index];

            var prop = randomGenerator.Next(10000);
            for (int i = 0; i < _gachaProp.Count; ++i)
            {
                prop -= _gachaProp[i].Value;
                if (prop <= 0)
                {
                    var grade = (GradeType)i;
                    var skillsTemplate = groupTemplate[grade];
                    if (skillsTemplate == null)
                    {
                        return groupTemplate.NormalRef.Id;
                    }
                    else
                    {
                        return skillsTemplate.Id;
                    }
                }
            }

            return BaseTemplate.InvidateTemplateId;
        }
    }
}
