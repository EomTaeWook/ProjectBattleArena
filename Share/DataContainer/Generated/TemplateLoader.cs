﻿using TemplateContainers;

namespace DataContainer.Generated
{
    public class TemplateLoader
    {
        public static void Load(string path)
        {
            TemplateContainer<CharacterTemplate>.Load(path, "Character.json");
            TemplateContainer<LevelUpTemplate>.Load(path, "LevelUp.json");
            TemplateContainer<SkillEffectsTemplate>.Load(path, "SkillEffects.json");
            TemplateContainer<SkillEffectsAbnormalStatusTemplate>.Load(path, "SkillEffectsAbnormalStatus.json");
            TemplateContainer<SkillEffectsDamageTemplate>.Load(path, "SkillEffectsDamage.json");
            TemplateContainer<SkillsTemplate>.Load(path, "Skills.json");
        }
        public static void MakeRefTemplate()
        {
            TemplateContainer<CharacterTemplate>.MakeRefTemplate();
            TemplateContainer<LevelUpTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsAbnormalStatusTemplate>.MakeRefTemplate();
            TemplateContainer<SkillEffectsDamageTemplate>.MakeRefTemplate();
            TemplateContainer<SkillsTemplate>.MakeRefTemplate();
        }
    }
}
