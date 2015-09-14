using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Helpers
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

        /// <summary>
        /// Determines if points are on the same axis (either X or Y). Basically it checks if either their X or Y coordinates are matching
        /// </summary>
        /// <param name="testinPoint"></param>
        /// <param name="referencePoint"></param>
        /// <returns></returns>
        public static bool IsOnSameAxis(this Point a, Point b)
        {
            return a.X == b.X || a.Y == b.Y;
        }
    }
}