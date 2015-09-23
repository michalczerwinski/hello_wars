using System;
using System.Drawing;
using System.Linq;
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.Services
{
    internal class LocationService
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private readonly GameArena _arena;

        public LocationService(GameArena arena)
        {
            _arena = arena;
        }

        public Point GetRandomEmptyPointOnBoard()
        {
            var result = new Point();
            while (true)
            {
                result.X = _rand.Next(_arena.Board.GetLength(0));
                result.Y = _rand.Next(_arena.Board.GetLength(1));

                if (_arena.Board[result.X, result.Y] == BoardTile.Empty && _arena.Bots.All(bot => bot.Location != result))
                {
                    return result;
                }
            }
        }

        public bool IsLocationAvailableForMissile(Point location)
        {
            return IsLocationValid(location) && _arena.Board[location.X, location.Y] == BoardTile.Empty && _arena.Bots.All(blasterBot => blasterBot.Location != location) &&
                   _arena.Bombs.All(bomb => bomb.Location != location) && _arena.Missiles.All(m => m.Location != location);
        }

        public Point GetNewLocation(Point oldLocation, MoveDirection? direction)
        {
            var newLocation = new Point(oldLocation.X, oldLocation.Y);

            if (direction == null)
            {
                return newLocation;
            }

            switch (direction)
            {
                case MoveDirection.Up:
                    newLocation.Y--;
                    break;
                case MoveDirection.Down:
                    newLocation.Y++;
                    break;
                case MoveDirection.Right:
                    newLocation.X++;
                    break;
                case MoveDirection.Left:
                    newLocation.X--;
                    break;
            }

            return newLocation;
        }

        public bool IsLocationValid(Point location)
        {
            return location.X >= 0
                   && location.X < _arena.Board.GetLength(0)
                   && location.Y >= 0
                   && location.Y < _arena.Board.GetLength(1);
        }
    }
}