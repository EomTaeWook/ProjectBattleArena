namespace ShareLogic
{
    public class CharacterStatHelper
    {
        private const int MagicNumber = 325;
        private const int MagicNumber2 = 786;
        public static float GetHitRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return Calculate(currentValue, maxValue);
        }
        public static float GetDodgeRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return Calculate(currentValue, maxValue);
        }
        public static float GetCriticalRate(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber);
            return Calculate(currentValue, maxValue);
        }
        public static float GetCriticalResistance(int level, int currentValue)
        {
            var maxValue = (level * MagicNumber2);
            return Calculate(currentValue, maxValue);
        }
        public static float GetBlockRate(int level, int currentValue)
        {
            var maxValue = level * MagicNumber2;
            return Calculate(currentValue, maxValue);
        }
        public static float GetBlockPenetration(int level, int currentValue)
        {
            var maxValue = level * MagicNumber;
            return Calculate(currentValue, maxValue);
        }
        public static float GetCriticalDamgagePercent(int level, int currentValue)
        {
            var maxValue = level * MagicNumber2;
            return Calculate(currentValue, maxValue);
        }
        private static float Calculate(int currentValue, int maxValue)
        {
            return (float)(currentValue * 1.0 / maxValue * 100);
        }
    }
}
