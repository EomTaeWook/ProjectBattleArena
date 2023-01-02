using Kosher.Framework;
using Protocol.GameWebServerAndClient.ShareModels;
using System.Collections.Generic;

public class BattleManager : Singleton<BattleManager>
{
    public Battle MakeBattle(int seed,
        List<CharacterData> allies,
        List<CharacterData> enemies
        )
    {
        BattleEventHandler eventHandler = new BattleEventHandler();
        var battle = new Battle(eventHandler,
            seed,
            allies,
            enemies
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