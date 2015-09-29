using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Helpers.TankBlaster
{
    public class TankBlasterSimpleAIService
    {
        private BotArenaInfo _field;
        
        private readonly bool _shouldDropBombs;
        private readonly bool _shouldFireMissiles;
        private readonly int _bombDroppingFrequency;
        private readonly int _missileFiringFrequency;

        private readonly List<MoveDirection> _allMoveDirections = Enum.GetValues(typeof(MoveDirection)).Cast<MoveDirection>().ToList();

        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public TankBlasterSimpleAIService(bool shouldDropBombs, bool shouldFireMissiles, int bombDroppingFrequency, int missileFiringFrequency)
        {
            _shouldDropBombs = shouldDropBombs;
            _shouldFireMissiles = shouldFireMissiles;
            _bombDroppingFrequency = bombDroppingFrequency;
            _missileFiringFrequency = missileFiringFrequency;
        }

        public BotMove CalculateNextMove(BotArenaInfo arenaInfo)
        {
            _field = arenaInfo;
            var result = new BotMove();

            if (_shouldDropBombs && _rand.Next(_bombDroppingFrequency) == 0)
            {
                result.Action = BotAction.DropBomb;
            }

            if (_shouldFireMissiles && _rand.Next(_missileFiringFrequency) == 0)
            {
                var fireDirections = _allMoveDirections.Where(direction =>
                {
                    var tempLocation = _field.BotLocation;
                    for (int i = 0; i < CurrentMissileBlastRadius + 1; i++)
                    {
                        tempLocation = AddDirectionMove(tempLocation, direction);
                        if (!IsLocationValid(tempLocation) || _field.Board[tempLocation.X, tempLocation.Y] != BoardTile.Empty)
                        {
                            return false;
                        }
                    }
                    return true;
                }).ToList();

                if (fireDirections.Any())
                {
                    result.Action = BotAction.FireMissile;
                    result.FireDirection = fireDirections.ElementAt(_rand.Next(fireDirections.Count()));
                }
            }

            if (IsInDangerZone(_field.BotLocation))
            {
                result.Action = BotAction.None;
            }

            var directions = _allMoveDirections.Where(direction => !IsInDangerZone(AddDirectionMove(_field.BotLocation, direction))).ToList();

            if (!directions.Any())
            {
                directions = _allMoveDirections;
            }
            result.Direction = result.Action == BotAction.FireMissile ? (MoveDirection?)null : directions[_rand.Next(directions.Count)];

            return result;
        }

        private int CurrentMissileBlastRadius
        {
            get
            {
                return _field.GameConfig.MissileBlastRadius +
                       (_field.GameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (_field.RoundNumber / _field.GameConfig.RoundsBeforeIncreasingBlastRadius));
            }
        }

        private bool IsInDangerZone(Point location)
        {
            return !IsLocationValid(location) || _field.Bombs.SelectMany(bomb => GetDangerZone(bomb.Location, bomb.ExplosionRadius)).Contains(location) ||
                   _field.Missiles.SelectMany(missile => GetDangerZone(missile.Location, missile.ExplosionRadius)).Contains(location) || _field.Board[location.X, location.Y] != BoardTile.Empty;
        }

        private List<Point> GetDangerZone(Point centerLocation, int explosionRadius)
        {
            var result = GetSurroundingPoints(centerLocation, explosionRadius).ToList();
            result.Add(centerLocation);

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

                foreach (var point in locations.Where(IsLocationValid))
                {
                    yield return point;
                }
            }
        }

        private bool IsLocationValid(Point location)
        {
            return location.X >= 0
                   && location.X < _field.Board.GetLength(0)
                   && location.Y >= 0
                   && location.Y < _field.Board.GetLength(1);
        }

        private Point AddDirectionMove(Point location, MoveDirection direction)
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
    }
}