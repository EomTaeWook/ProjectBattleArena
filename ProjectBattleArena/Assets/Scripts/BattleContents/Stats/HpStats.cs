namespace GameContents
{
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
            if (Shield < 0)
            {
                Shield = 0;
            }
        }
        public void ModifyHp(int diff)
        {
            CurrentHp += diff;
            if (CurrentHp < 0)
            {
                CurrentHp = 0;
            }
        }
        public static int GetMaxHP(int initHp, int level, int conStat)
        {
            var levelHp = level * 10;
            var statHp = conStat * 3;
            return initHp + levelHp + statHp;
        }
    }
}

