using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Helpers.DynaBlaster
{
    public class LocationService
    {
        private readonly BotArenaInfo _arena;
        public readonly List<MoveDirection> AllMoveDirections = Enum.GetValues(typeof(MoveDirection)).Cast<MoveDirection>().ToList();

        public LocationService(BotArenaInfo arena)
        {
            _arena = arena;
        }

        public IEnumerable<Point> GetSurroundingPoints(Point centerLocation, int radius)
        {
            for (int i = 1; i <= radius; i++)
            {
                var locations = new[]
                {
                    new Point(centerLocation.X, centerLocation.Y + i),
                    new Point(centerLocation.X, centerLocation.Y - i),
                    new Point(centerLocation.X + i, centerLocation.Y),
                    new Point(centerLocation.X - i, centerLocation.Y)
                };

                foreach (var point in locations.Where(IsValidLocation))
                {
                    yield return point;
                }
            }
        }

        public bool IsValidLocation(Point location)
        {
            return location.X >= 0 && location.Y >= 0 && location.X < _arena.Board.GetLength(0) && location.Y < _arena.Board.GetLength(1);
        }

        public bool IsBlocked(Point location, bool includeExplodables = false)
        {
            return _arena.Board[location.X, location.Y] != BoardTile.Empty && _arena.OpponentLocations.All(point => point != location) &&
                   (!includeExplodables || (_arena.Bombs.All(bomb => bomb.Location != location) && _arena.Missiles.All(missile => missile.Location != location)));
        }

        public List<MoveDirection> MatchingDirections(Point startingPoint, Point destinationPoint)
        {
            var currentDistance = startingPoint.DistanceFrom(destinationPoint);

            return
                AllMoveDirections.Where(
                    direction =>
                        IsValidLocation(startingPoint.AddDirectionMove(direction)) && startingPoint.AddDirectionMove(direction).DistanceFrom(destinationPoint) < currentDistance)
                    .OrderByDescending(direction => startingPoint.DistanceFromInDirection(destinationPoint, direction))
                    .ToList();
        }
    }
}