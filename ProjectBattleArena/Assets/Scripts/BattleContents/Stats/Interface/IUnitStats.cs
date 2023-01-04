namespace GameContents
{
    public interface IUnitStats
    {
        public int Attack { get; }

        public int Defense { get; }

        public float AttackSpeed { get; }

        public float CriticalRate { get; }

        public float CriticalResistance { get; }

        public float CriticalDamage { get; }

        public float BlockRate { get; }

        public float BlockPenetration { get; }

        public float HitRate { get; }

        public float DodgeRate { get; }
    }
}
