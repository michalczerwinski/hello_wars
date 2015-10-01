using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.TankBlaster.Models;

namespace Game.TankBlaster.Services
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

        public bool IsPassageBetweenTwoPlayers()
        {
            var matrix = new int[_field.Board.GetLength(0), _field.Board.GetLength(1)];
            var firstBot = _field.Bots[0].Location;
            var botPositions = _field.Bots.Select(t => t.Location).ToList();
            botPositions.Remove(firstBot);

            return CheckNeighborhood(firstBot.X, firstBot.Y, matrix, botPositions);
        }

        private bool CheckNeighborhood(int x, int y, Int32[,] matrix, List<Point> botPositions)
        {
            if (!IsLocationValid(new Point { X = x, Y = y }))
            {
                return false;
            }

            if (botPositions.Contains(new Point { X = x, Y = y }))
            {
                return true;
            }

            if ((_field.Board[x, y] != BoardTile.Indestructible) && (matrix[x, y] != 1))
            {
                matrix[x, y] = 1;

                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var location in GetAdjacentLocations(new Point(x, y)))
                {
                    var botFound = CheckNeighborhood(location.X, location.Y, matrix, botPositions);
                    if (botFound)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}