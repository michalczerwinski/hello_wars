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
        private int _desiredDistanceFromOpponent = 4;
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private static readonly List<MoveDirection> _allDirections = Enum.GetValues(typeof (MoveDirection)).Cast<MoveDirection>().ToList();
        private Objective _currentObjective;
        private Point _closestOpponentLocation;

        private int CurrentBombBlastRadius
        {
            get { return _arena.GameConfig.BombBlastRadius + (_arena.GameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (_arena.RoundNumber / _arena.GameConfig.RoundsBeforeIncreasingBlastRadius)); }
        }

        private int CurrentMissileBlastRadius
        {
            get { return _arena.GameConfig.MissileBlastRadius + (_arena.GameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (_arena.RoundNumber / _arena.GameConfig.RoundsBeforeIncreasingBlastRadius)); }
        }

        public BotMove CalculateNextMove(BotArenaInfo arena, List<Point> previousLocations)
        {
            _arena = arena;
            _desiredDistanceFromOpponent = CurrentMissileBlastRadius + 4;
            _dangerMap = CalculateDangerMap();

            var result = new BotMove()
            {
                Action = _rand.Next(7) == 0 ?  BotAction.DropBomb : BotAction.None
            };

            _closestOpponentLocation = _arena.OpponentLocations.OrderBy(point => point.DistanceFrom(_arena.BotLocation)).First();
            
            var safestDirections = SafestDirections(_arena.BotLocation).ToList();

            var currentObjective = GetCurrentObjective();
            List<MoveDirection> objectiveDirections = new List<MoveDirection>();
            switch (currentObjective)
            {
                case Objective.GetCloserToOpponent:
                    objectiveDirections = MatchingDirections(_arena.BotLocation, _closestOpponentLocation);
                    break;
                case Objective.GetIntoFiringPosition:
                    objectiveDirections = _allDirections;
                    break;
                case Objective.DestroyThatSucker:
                    if (IsSafe(_arena.BotLocation))
                    {
                        result.Action = BotAction.FireMissile;
                        result.FireDirection = MatchingDirections(_arena.BotLocation, _closestOpponentLocation).First();
                        result.Direction = null;
                        return result;
                    }
                    break;
            }

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

        #region Movement methods

        private List<MoveDirection> MatchingDirections(Point startingPoint, Point destinationPoint)
        {
            var currentDistance = startingPoint.DistanceFrom(destinationPoint);

            return
                _allDirections.Where(
                    direction =>
                        IsValidLocation(startingPoint.AddDirectionMove(direction)) && startingPoint.AddDirectionMove(direction).DistanceFrom(destinationPoint) < currentDistance)
                    .OrderByDescending(direction => startingPoint.DistanceFromInDirection(destinationPoint, direction))
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

        #endregion

        #region Location methods

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

        private bool IsValidLocation(Point location)
        {
            return location.X >= 0 && location.Y >= 0 && location.X < _arena.Board.GetLength(0) && location.Y < _arena.Board.GetLength(1);
        }

        private bool IsBlocked(Point location, bool includeExplodables = false)
        {
            return _arena.Board[location.X, location.Y] != BoardTile.Empty && _arena.OpponentLocations.All(point => point != location) &&
                   (!includeExplodables || (_arena.Bombs.All(bomb => bomb.Location != location) && _arena.Missiles.All(missile => missile.Location != location)));
        }

        #endregion

        #region Danger calculation methods

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
            dangerLocations.AddRange(_arena.Missiles.SelectMany(missile => GetBombDangerZone(missile.Location)).Where(point => !IsBlocked(point)));

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

        private List<Point> GetBombDangerZone(Point centerLocation)
        {
            var result = GetSurroundingPoints(centerLocation, CurrentBombBlastRadius).ToList();
            result.Add(centerLocation);

            return result;
        }

        private bool CanSafelyFire(Point location, MoveDirection direction)
        {
            var tempLocation = new Point(location.X, location.Y);
            for (int i = 0; i < CurrentMissileBlastRadius; i++)
            {
                tempLocation = tempLocation.AddDirectionMove(direction);
                if (!IsValidLocation(tempLocation) || IsBlocked(tempLocation, true) || !IsSafe(tempLocation))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFiringPosition(Point location)
        {
            return location.DistanceFrom(_closestOpponentLocation) >= _desiredDistanceFromOpponent &&
                   (location.IsOnSameAxis(_closestOpponentLocation) || GetSurroundingPoints(_closestOpponentLocation, 1).Any(point => point.IsOnSameAxis(location))) &&
                   CanSafelyFire(location, MatchingDirections(location, _closestOpponentLocation).First());
        }

        private bool IsSafe(Point location)
        {
            return _dangerMap[location.X, location.Y] == 0;
        }

        #endregion

        #region Objective methods

        private Objective GetCurrentObjective()
        {
            if (IsFiringPosition(_arena.BotLocation) && _arena.IsMissileAvailable)
            {
                return Objective.DestroyThatSucker;
            }

            if (_arena.BotLocation.DistanceFrom(_closestOpponentLocation) > _desiredDistanceFromOpponent)
            {
                return Objective.GetCloserToOpponent;
            }

            return Objective.GetIntoFiringPosition;
        }

        #endregion
    }
}