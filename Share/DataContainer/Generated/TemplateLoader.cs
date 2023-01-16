using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public partial class TemplateLoader
    {
        public static void Load(string path)
        {
            TemplateContainer<CharacterTemplate>.Load(path, "Character.json");
            TemplateContainer<ConstantTemplate>.Load(path, "Constant.json");
            TemplateContainer<GoodsTemplate>.Load(path, "Goods.json");
            TemplateContainer<LevelUpTemplate>.Load(path, "LevelUp.json");
            TemplateContainer<SkillEffectsTemplate>.Load(path, "SkillEffects.json");
            TemplateContainer<SkillEffectsAbnormalStatusTemplate>.Load(path, "SkillEffectsAbnormalStatus.json");
            TemplateContainer<SkillEffectsDamageTemplate>.Load(path, "SkillEffectsDamage.json");
            TemplateContainer<SkillsTemplate>.Load(path, "Skills.json");
            TemplateContainer<SkillsGroupTemplate>.Load(path, "SkillsGroup.json");
        }
        public static void Load(Func<string, string> funcLoadJson)
        {
            TemplateContainer<CharacterTemplate>.Load("Character.json", funcLoadJson);
            TemplateContainer<ConstantTemplate>.Load("Constant.json", funcLoadJson);
            TemplateContainer<GoodsTemplate>.Load("Goods.json", funcLoadJson);
            TemplateContainer<LevelUpTemplate>.Load("LevelUp.json", funcLoadJson);
            TemplateContainer<SkillEffectsTemplate>.Load("SkillEffects.json", funcLoadJson);
            TemplateContainer<SkillEffectsAbnormalStatusTemplate>.Load("SkillEffectsAbnormalStatus.json", funcLoadJson);
            TemplateContainer<SkillEffectsDamageTemplate>.Load("SkillEffectsDamage.json", funcLoadJson);
            TemplateContainer<SkillsTemplate>.Load("Skills.json", funcLoadJson);
            TemplateContainer<SkillsGroupTemplate>.Load("SkillsGroup.json", funcLoadJson);
        }
        public static void MakeRefTemplate()
        {
            TemplateContainer<CharacterTemplate>.MakeRefTemplate();
            TemplateContainer<ConstantTemplate>.MakeRefTemplate();
            TemplateContainer<GoodsTemplate>.MakeRefTemplate();
            TemplateContainer<LevelUpTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsAbnormalStatusTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsDamageTemplate>.MakeRefTemplate();
            TemplateContainer<SkillsTemplate>.MakeRefTemplate();
            TemplateContainer<SkillsGroupTemplate>.MakeRefTemplate();
        }
    }
}
