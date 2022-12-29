public class HpStats
{
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public int Shield { get; private set; }

    public HpStats(int maxHp)
    {
        MaxHp = maxHp;
        CurrentHp = MaxHp;
    }
    public void ModifyShield(int diff)
    {
        Shield += diff;
    }
    public void ModifyHp(int diff)
    {
        CurrentHp += diff;
    }

    public static int GetMaxHP(int initHp, int level, int conStat)
    {
        var levelHp = level * 10;
        var statHp = conStat * 3;
        return initHp + levelHp + statHp;
    }
}