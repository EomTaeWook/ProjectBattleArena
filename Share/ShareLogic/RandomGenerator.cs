using System;

namespace ShareLogic.Random
{
    public class RandomGenerator
    {
        private readonly System.Random _random = new System.Random();
        public RandomGenerator()
        {
            _random = new System.Random(DateTime.Now.Ticks.GetHashCode());
        }
        public RandomGenerator(int seed)
        {
            _random = new System.Random(seed);
        }
        public int Next()
        {
            return _random.Next();
        }
        public int Next(int min, int max)
        {
            if (max >= int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }
            return _random.Next(min, max + 1);
        }
        public int Next(int max)
        {
            return Next(0, max);
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}
