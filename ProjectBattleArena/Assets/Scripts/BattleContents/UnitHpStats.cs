public class UnitHpStats
{
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }

    public int Shield { get; private set; }

    public UnitHpStats(int maxHp)
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
}