namespace ShareLogic
{
    public class CharacterStatHelper
    {
        private const int MagicNumber = 325;
        private const int MagicNumber2 = 786;
        public static float GetHitRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return currentValue / maxValue * 100;
        }
        public static float GetDodgeRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return currentValue / maxValue * 100;
        }
        public static float GetCriticalRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return currentValue / maxValue * 100;
        }
        public static float GetCriticalResistance(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber2);
            return currentValue / maxValue * 100;
        }
        public static float GetBlockRate(int level, int currentValue)
        {
            var maxValue = level * MagicNumber2;
            return currentValue / maxValue * 100;
        }
        public static float GetBlockPenetration(int level, int currentValue)
        {
            var maxValue = level * MagicNumber;
            return currentValue / maxValue * 100;
        }
        public static float GetCriticalDamgagePercent(int level, int currentValue)
        {
            var maxValue = level * MagicNumber2;
            return currentValue / maxValue * 100;
        }
    }
}
