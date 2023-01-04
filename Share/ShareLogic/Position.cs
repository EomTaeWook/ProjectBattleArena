using System;

namespace ShareLogic
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public static Position operator +(Position position1, Position position2)
        {
            var position = new Position
            {
                X = position1.X + position2.X,
                Y = position1.Y + position2.Y
            };
            return position;
        }
        public static Position operator -(Position position1, Position position2)
        {
            var position = new Position
            {
                X = position1.X - position2.X,
                Y = position1.Y - position2.Y
            };
            return position;
        }

        public Position Normalize()
        {
            var powX = Math.Pow(this.X, 2);

            var powY = Math.Pow(this.Y, 2);

            var sqrt = Math.Sqrt(powX + powY);

            var normalizePostion = new Position()
            {
                X = (int)(X / sqrt),
                Y = (int)(Y / sqrt)
            };

            return normalizePostion;
        }
    }
}
