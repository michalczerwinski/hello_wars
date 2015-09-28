using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Helpers.TankBlaster
{
    public class CombatService
    {
        private readonly BotArenaInfo _arena;
        private readonly int _desiredDistanceFromOpponent;
        private readonly LocationService _locationService;

        public CombatService(BotArenaInfo arena, LocationService locationService)
        {
            _arena = arena;
            _locationService = locationService;
            _desiredDistanceFromOpponent = CurrentMissileBlastRadius + 4;
            ClosestOpponentLocation = _arena.OpponentLocations.OrderBy(point => point.DistanceFrom(_arena.BotLocation)).First();
            CalculateDangerMap();
        }

        public Point ClosestOpponentLocation { get; private set; }
        public int[,] DangerMap { get; private set; }

        private int CurrentBombBlastRadius
        {
            get
            {
                return _arena.GameConfig.BombBlastRadius +
                       (_arena.GameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (_arena.RoundNumber/_arena.GameConfig.RoundsBeforeIncreasingBlastRadius));
            }
        }

        private int CurrentMissileBlastRadius
        {
            get
            {
                return _arena.GameConfig.MissileBlastRadius +
                       (_arena.GameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (_arena.RoundNumber/_arena.GameConfig.RoundsBeforeIncreasingBlastRadius));
            }
        }

        public Objective GetCurrentObjective()
        {
            if (IsFiringPosition(_arena.BotLocation) && _arena.IsMissileAvailable)
            {
                return Objective.DestroyThatSucker;
            }

            if (_arena.BotLocation.DistanceFrom(ClosestOpponentLocation) > _desiredDistanceFromOpponent)
            {
                return Objective.GetCloserToOpponent;
            }

            return Objective.GetIntoFiringPosition;
        }

        public bool IsSafe(Point location)
        {
            return DangerMap[location.X, location.Y] == 0;
        }

        public IEnumerable<MoveDirection> SafestDirections(Point startingPoint)
        {
            var candidateDestinations = _locationService.GetSurroundingPoints(startingPoint, 1).ToList();

            var bestDestinations = candidateDestinations.GroupBy(point => DangerMap[point.X, point.Y]).OrderBy(points => points.Key).First();

            if (DangerMap[startingPoint.X, startingPoint.Y] < bestDestinations.Key)
            {
                return new List<MoveDirection>();
            }

            return bestDestinations.SelectMany(destination => _locationService.MatchingDirections(startingPoint, destination));
        }

        /// <summary>
        ///     Calculates map of danger zones. Each tile is assigned with a number.
        ///     0 is safe zone, >0 is danger zone (the value of tile indicates
        ///     number of moves to closest safe zone). Int32.Max indicates wall
        ///     100 is certain death location (blast zone of bomb that will go off
        ///     during next round)
        /// </summary>
        /// <returns></returns>
        private void CalculateDangerMap()
        {
            DangerMap = new int[_arena.Board.GetLength(0), _arena.Board.GetLength(1)];

            for (var i = 0; i < DangerMap.GetLength(0); i++)
            {
                for (var j = 0; j < DangerMap.GetLength(1); j++)
                {
                    DangerMap[i, j] = _arena.Board[i, j] == BoardTile.Empty ? 0 : int.MaxValue;
                }
            }

            var dangerLocations = _arena.Bombs.SelectMany(bomb => GetBombDangerZone(bomb.Location)).Where(point => !_locationService.IsBlocked(point)).ToList();
            dangerLocations.AddRange(_arena.Missiles.SelectMany(missile => GetBombDangerZone(missile.Location)).Where(point => !_locationService.IsBlocked(point)));

            var certainDeathLocations =
                _arena.Bombs.Where(bomb => bomb.RoundsUntilExplodes == 1)
                    .SelectMany(bomb => GetBombDangerZone(bomb.Location))
                    .Where(point => !_locationService.IsBlocked(point))
                    .ToList();

            foreach (var location in dangerLocations)
            {
                DangerMap[location.X, location.Y] = 10;
            }

            var iterations = _arena.Bombs.Any() ? _arena.Bombs.Max(bomb => bomb.RoundsUntilExplodes) : 0;

            for (var i = 0; i < iterations; i++)
            {
                foreach (var location in dangerLocations)
                {
                    var minSurroundingDangerFactor = _locationService.GetSurroundingPoints(location, 1).Min(point => DangerMap[point.X, point.Y]);

                    DangerMap[location.X, location.Y] = minSurroundingDangerFactor + 1;
                }
            }

            foreach (var certainDeathLocation in certainDeathLocations)
            {
                DangerMap[certainDeathLocation.X, certainDeathLocation.Y] = 100;
            }
        }

        private bool CanSafelyFire(Point location, MoveDirection direction)
        {
            var tempLocation = new Point(location.X, location.Y);
            for (var i = 0; i <= Math.Min(CurrentMissileBlastRadius, 4); i++)
            {
                tempLocation = tempLocation.AddDirectionMove(direction);
                if (!_locationService.IsValidLocation(tempLocation) || _locationService.IsBlocked(tempLocation, true) || !IsSafe(tempLocation))
                {
                    return false;
                }
            }

            return true;
        }

        private List<Point> GetBombDangerZone(Point centerLocation)
        {
            var result = _locationService.GetSurroundingPoints(centerLocation, CurrentBombBlastRadius).ToList();
            result.Add(centerLocation);

            return result;
        }

        private bool IsFiringPosition(Point location)
        {
            return location.DistanceFrom(ClosestOpponentLocation) >= _desiredDistanceFromOpponent &&
                   (location.IsOnSameAxis(ClosestOpponentLocation) || _locationService.GetSurroundingPoints(ClosestOpponentLocation, 1).Any(point => point.IsOnSameAxis(location))) &&
                   CanSafelyFire(location, _locationService.MatchingDirections(location, ClosestOpponentLocation).First());
        }
    }
}