using ShareLogic;

namespace GameContents
{
    public class UnitStats : IUnitStats
    {
        //atk - 공격 명중 파괴
        //con - 생명력, 차단, 회피
        //Dex - 방어 치명타, 인내
        public UnitStats(UnitInfo unitInfo)
        {
            Level = unitInfo.Level;

            var maxHp = unitInfo.CharacterTemplate.Stat.HP + (unitInfo.Con);
            Hp = new HpStats(maxHp);

            Attack = unitInfo.CharacterTemplate.Stat.Attack + (unitInfo.Atk);

            Defense = unitInfo.CharacterTemplate.Stat.Defense + (unitInfo.Dex);

            AttackSpeed = unitInfo.CharacterTemplate.Stat.AttackSpeed;

            CriticalRate = CharacterStatHelper.GetCriticalRate(Level,
                unitInfo.CharacterTemplate.Stat.Critical + (unitInfo.Dex));

            CriticalResistance = CharacterStatHelper.GetCriticalResistance(Level,
                                    unitInfo.CharacterTemplate.Stat.Patience + unitInfo.Dex);

            CriticalDamage = CharacterStatHelper.GetCriticalDamgagePercent(Level,
                                    unitInfo.CharacterTemplate.Stat.Destruction + unitInfo.Atk);

            BlockRate = CharacterStatHelper.GetBlockRate(Level, unitInfo.CharacterTemplate.Stat.Block + unitInfo.Con);

            BlockPenetration = CharacterStatHelper.GetBlockPenetration(Level, unitInfo.CharacterTemplate.Stat.Destruction + unitInfo.Atk);

            HitRate = CharacterStatHelper.GetHitRate(Level, unitInfo.CharacterTemplate.Stat.Hit + unitInfo.Atk);

            DodgeRate = CharacterStatHelper.GetDodgeRate(Level, unitInfo.CharacterTemplate.Stat.Dodge + unitInfo.Con);
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

        public float DodgeRate { get; private set; }
    }
}