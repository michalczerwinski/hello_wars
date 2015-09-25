using System;
using System.Drawing;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Helpers.DynaBlaster
{
    public static class DynaBlasterExtensions
    {
        public static Point AddDirectionMove(this Point location, MoveDirection direction)
        {
            var result = new Point(location.X, location.Y);
            switch (direction)
            {
                case MoveDirection.Up:
                    result.Y--;
                    break;
                case MoveDirection.Down:
                    result.Y++;
                    break;
                case MoveDirection.Right:
                    result.X++;
                    break;
                case MoveDirection.Left:
                    result.X--;
                    break;
            }

            return result;
        }

        public static int DistanceFrom(this Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static int DistanceFromInDirection(this Point a, Point b, MoveDirection direction)
        {
            if (a.AddDirectionMove(direction).DistanceFrom(b) > a.DistanceFrom(b))
            {
                return int.MaxValue;
            }

            return (direction == MoveDirection.Down || direction == MoveDirection.Up) ? Math.Abs(a.Y - b.Y) : Math.Abs(a.X - b.X);
        }

        /// <summary>
        /// Determines if points are on the same axis (either X or Y). Basically it checks if either their X or Y coordinates
        /// are matching
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsOnSameAxis(this Point a, Point b)
        {
            return a.X == b.X || a.Y == b.Y;
        }
    }
}