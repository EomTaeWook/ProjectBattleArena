using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TemplateContainers;

namespace DataContainer.Generated
{
    public class TemplateLoader
    {
        public static void Load(string path)
        {
            TemplateContainer<CharacterTemplate>.Load(path, "Character.json");
            TemplateContainer<LevelUpTemplate>.Load(path, "LevelUp.json");
            TemplateContainer<SkillsTemplate>.Load(path, "Skills.json");
        }
        public static void MakeRefTemplate()
        {
            TemplateContainer<CharacterTemplate>.MakeRefTemplate();
            TemplateContainer<LevelUpTemplate>.MakeRefTemplate();
            TemplateContainer<SkillsTemplate>.MakeRefTemplate();
        }
    }
}
