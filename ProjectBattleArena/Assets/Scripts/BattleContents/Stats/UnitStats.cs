using DataContainer.Generated;
using Protocol.GameWebServerAndClient.ShareModels;
using ShareLogic;

public class UnitStats : IUnitStats
{        
    //atk - 공격 명중 파괴
    //con - 생명력, 차단, 회피
    //Dex - 방어 치명타, 인내
    public UnitStats(CharacterTemplate characterTemplate,
                    CharacterData characterData)
    {
        Level = LevelUpHelper.GetLevel(characterData.Exp);

        var maxHp = characterTemplate.Stat.HP + (characterData.Con);
        Hp = new HpStats(maxHp);

        Attack = characterTemplate.Stat.Attack + (characterData.Atk);

        Defense = characterTemplate.Stat.Defense + (characterData.Dex);

        AttackSpeed = characterTemplate.Stat.AttackSpeed;

        CriticalRate = CharacterStatHelper.GetCriticalRate(Level,
            characterTemplate.Stat.Critical + (characterData.Dex));

        CriticalResistance = CharacterStatHelper.GetCriticalResistance(Level,
                                characterTemplate.Stat.Patience + characterData.Dex);

        CriticalDamage = CharacterStatHelper.GetCriticalDamgagePercent(Level,
                                characterTemplate.Stat.Destruction + characterData.Atk);

        BlockRate = CharacterStatHelper.GetBlockRate(Level, characterTemplate.Stat.Block + characterData.Con);

        BlockPenetration = CharacterStatHelper.GetBlockPenetration(Level, characterTemplate.Stat.Destruction + characterData.Atk);

        HitRate = CharacterStatHelper.GetHitRate(Level, characterTemplate.Stat.Hit + characterData.Atk);

        Dodge = CharacterStatHelper.GetDodgeRate(Level, characterTemplate.Stat.Hit + characterData.Con);
    }

    public int Level { get; set; }

    public HpStats Hp { get; private set; }

    public int Attack { get; private set; }

    public int Defense { get; private set; }

    public float AttackSpeed { get; private set; }

    public float CriticalRate { get; private set; }

    public float CriticalResistance { get; private set; }

    public float CriticalDamage { get; private set; }

    public float BlockRate { get; private set; }

    public float BlockPenetration { get; private set; }

    public float HitRate { get; private set; }

    public float Dodge { get; private set; }
}