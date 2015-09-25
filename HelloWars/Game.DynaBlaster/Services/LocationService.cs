using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.Services
{
    internal class LocationService
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private readonly Battlefield _field;

        public LocationService(Battlefield field)
        {
            _field = field;
        }

        public Point GetRandomEmptyPointOnBoard()
        {
            var result = new Point();
            while (true)
            {
                result.X = _rand.Next(_field.Board.GetLength(0));
                result.Y = _rand.Next(_field.Board.GetLength(1));

                if (_field.Board[result.X, result.Y] == BoardTile.Empty && _field.Bots.All(bot => bot.Location != result))
                {
                    return result;
                }
            }
        }

        public List<Point> GetAdjacentLocations(Point startingLocation)
        {
            return Enum.GetValues(typeof (MoveDirection)).Cast<MoveDirection>().Select(direction => GetNewLocation(startingLocation, direction)).ToList();
        } 

        public bool IsLocationAvailableForMissile(Point location)
        {
            return IsLocationValid(location) && _field.Board[location.X, location.Y] == BoardTile.Empty && _field.Bots.All(blasterBot => blasterBot.Location != location) &&
                   _field.Bombs.All(bomb => bomb.Location != location) && _field.Missiles.All(m => m.Location != location);
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
                   && location.X < _field.Board.GetLength(0)
                   && location.Y >= 0
                   && location.Y < _field.Board.GetLength(1);
        }

        public bool IsLocationEmpty(Point location)
        {
            return IsLocationValid(location) 
                && _field.Board[location.X, location.Y] == BoardTile.Empty 
                && _field.Bots.All(bot => bot.Location != location) 
                && _field.Bombs.All(bomb => bomb.Location != location) 
                && _field.Missiles.All(missile => missile.Location != location);
        }
    }
}