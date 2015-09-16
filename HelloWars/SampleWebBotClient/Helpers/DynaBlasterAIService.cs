using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Helpers
{
    public class DynaBlasterAiService
    {
        private BotArenaInfo _arena;
        private int[,] _dangerMap;
        private const int _explosionRadius = 2;
        private const int _desiredDistanceFromOpponent = 2;
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private static readonly List<MoveDirection> _allDirections = Enum.GetValues(typeof (MoveDirection)).Cast<MoveDirection>().ToList();

        public BotMove CalculateNextMove(BotArenaInfo arena, List<Point> previousLocations)
        {
            _arena = arena;
            _dangerMap = CalculateDangerMap();

            var result = new BotMove()
            {
                Action = _rand.Next(5) == 0 ? BotAction.DropBomb : BotAction.None
            };

            var closestOpponent = _arena.OpponentLocations.OrderBy(point => point.DistanceFrom(_arena.BotLocation)).First();
            
            var safestDirections = SafestDirections(_arena.BotLocation).ToList();
            var objectiveDirections = ObjectiveDirections(_arena.BotLocation, closestOpponent);
            var viableDirections = safestDirections.Intersect(objectiveDirections).ToList();

            if (viableDirections.Any())
            {
                result.Direction = viableDirections[_rand.Next(viableDirections.Count)];
            }
            else
            {
                result.Direction = safestDirections.Any() ? safestDirections[_rand.Next(safestDirections.Count)] : (MoveDirection?)null;
            }

            return result;
        }

        private List<MoveDirection> ObjectiveDirections(Point botLocation, Point opponentLocation)
        {
            if (botLocation.DistanceFrom(opponentLocation) < _desiredDistanceFromOpponent)
            {
                return _allDirections;
            }

            return MatchingDirections(botLocation, opponentLocation);
        }

        private List<MoveDirection> MatchingDirections(Point startingPoint, Point destinationPoint)
        {
            var currentDistance = startingPoint.DistanceFrom(destinationPoint);

            return
                _allDirections.Where(
                    direction =>
                        IsValidLocation(startingPoint.AddDirectionMove(direction)) && startingPoint.AddDirectionMove(direction).DistanceFrom(destinationPoint) < currentDistance)
                    .ToList();
        }

        private IEnumerable<MoveDirection> SafestDirections(Point startingPoint)
        {
            var candidateDestinations = GetSurroundingPoints(startingPoint, 1).ToList();

            var bestDestinations = candidateDestinations.GroupBy(point => _dangerMap[point.X, point.Y]).OrderBy(points => points.Key).First();

            if (_dangerMap[startingPoint.X, startingPoint.Y] < bestDestinations.Key)
            {
                return new List<MoveDirection>();
            }

            return bestDestinations.SelectMany(destination => MatchingDirections(startingPoint, destination));
        }

        private bool IsValidLocation(Point location)
        {
            return location.X >= 0 && location.Y >= 0 && location.X < _arena.Board.GetLength(0) && location.Y < _arena.Board.GetLength(1);
        }

        private bool IsBlocked(Point location)
        {
            return _arena.Board[location.X, location.Y] != BoardTile.Empty;
        }

        /// <summary>
        /// Calculates map of danger zones. Each tile is assigned with a number.
        /// 0 is safe zone, >0 is danger zone (the value of tile indicates
        /// number of moves to closest safe zone). Int32.Max indicates wall
        /// 100 is certain death location (blast zone of bomb that will go off 
        /// during next round)
        /// </summary>
        /// <returns></returns>
        private int[,] CalculateDangerMap()
        {
            var result = new int[_arena.Board.GetLength(0), _arena.Board.GetLength(1)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = _arena.Board[i, j] == BoardTile.Empty ? 0 : int.MaxValue;
                }
            }

            var dangerLocations = _arena.Bombs.SelectMany(bomb => GetBombDangerZone(bomb.Location)).Where(point => !IsBlocked(point)).ToList();
            var certainDeathLocations =
                _arena.Bombs.Where(bomb => bomb.RoundsUntilExplodes == 1).SelectMany(bomb => GetBombDangerZone(bomb.Location)).Where(point => !IsBlocked(point)).ToList();

            foreach (var location in dangerLocations)
            {
                result[location.X, location.Y] = 10;
            }

            var iterations = _arena.Bombs.Any() ? _arena.Bombs.Max(bomb => bomb.RoundsUntilExplodes) : 0;

            for (var i = 0; i < iterations; i++)
            {
                foreach (var location in dangerLocations)
                {
                    var minSurroundingDangerFactor = GetSurroundingPoints(location, 1).Min(point => result[point.X, point.Y]);

                    result[location.X, location.Y] = minSurroundingDangerFactor + 1;
                }
            }

            foreach (var certainDeathLocation in certainDeathLocations)
            {
                result[certainDeathLocation.X, certainDeathLocation.Y] = 100;
            }

            return result;
        }

        private IEnumerable<Point> GetSurroundingPoints(Point centerLocation, int radius)
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

        private List<Point> GetBombDangerZone(Point centerLocation)
        {
            var result = GetSurroundingPoints(centerLocation, _explosionRadius).ToList();
            result.Add(centerLocation);

            return result;
        }
    }
}