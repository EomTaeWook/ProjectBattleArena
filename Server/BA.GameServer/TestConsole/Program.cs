using BA.GameServer.Battle;
using DataContainer.Generated;
using GameContents;
using TemplateContainers;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TemplateLoad();

            var ally = new List<UnitInfo>() { new UnitInfo()
            {
                CharacterName = "test",
                CharacterTemplate = TemplateContainer<CharacterTemplate>.Find(1000),
                Level = 1,
            }};
            var enemy = new List<UnitInfo>() { new UnitInfo()
            {
                CharacterName = "test2",
                CharacterTemplate = TemplateContainer<CharacterTemplate>.Find(1000),
                Level = 1,
            } };

            var battle = new Battle(new BattleEventHandler(),
                DateTime.Now.Ticks.GetHashCode(),
                ally,
                enemy);

            while (true)
            {
                if (battle.IsBattleEnd())
                {
                    break;
                }
                battle.ProcessTicks();
            }
        }
        private static void TemplateLoad()
        {
            var path = "../../Datas";
#if DEBUG
            path = $"{AppContext.BaseDirectory}../../../../../../Datas/";
#endif
            TemplateLoader.Load(path);
            TemplateLoader.MakeRefTemplate();
        }
    }
}