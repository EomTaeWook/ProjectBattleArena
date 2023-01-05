using BA.GameServer.Game;
using DataContainer.Generated;
using GameContents;
using Kosher.Log;
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

            var eventHandler = new BattleEventHandler();
            var battle = new Battle(eventHandler,
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

                var events = eventHandler.InvokedEvents();

                foreach(var item in events)
                {
                    var characterName = "null";

                    if(item.Item1 != null)
                    {
                        characterName = item.Item1.UnitInfo.CharacterName;
                    }
                    Console.WriteLine($"{characterName} - {item.Item2.GetType().Name}");
                }
                eventHandler.InvokedEvents().Clear();

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