using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.DynaBlaster.Models;
using Game.DynaBlaster.UserControls;
using Color = System.Windows.Media.Color;

namespace Game.DynaBlaster
{
    public class DynaBlaster : IGame
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private static int _explosionRadius = 2;
        private static int _delayTime;
        private GameArena _arena;
        private int _roundNumber;

        public DynaBlaster()
        {
            _arena = new GameArena();
        }

        #region IGame members

        public RoundResult PerformNextRound()
        {
            _arena.Explosions.Clear();
           
            foreach (var bomb in _arena.Bombs)
            {
                bomb.RoundsUntilExplodes--;
                if (!bomb.IsExploded && bomb.RoundsUntilExplodes == 0)
                {
                    SetExplosion(bomb);
                }
            }

            _arena.Bombs.RemoveAll(bomb => bomb.IsExploded);

            _arena.OnArenaChanged();

            DelayHelper.Delay(_delayTime);

            _arena.Explosions.Clear();

            if (_arena.Bots.Count(bot => !bot.IsDead) < 2)
            {
                return new RoundResult
                {
                    FinalResult = _arena.Bots.ToDictionary(competitor => competitor as ICompetitor, competitor => competitor.IsDead ? 0.0 : 1.0),
                    IsFinished = true,
                    History = new List<RoundPartialHistory>()
                };
            }

            foreach (var dynaBlasterBot in _arena.Bots.Where(bot => !bot.IsDead))
            {
                var move = dynaBlasterBot.NextMove(GetBotArenaInfo(dynaBlasterBot));

                if (IsMoveValid(dynaBlasterBot, move))
                {
                    PerformMove(dynaBlasterBot, move);
                    _arena.OnArenaChanged();
                }

                DelayHelper.Delay(_delayTime);
            }

            _roundNumber++;

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = new List<RoundPartialHistory>()
            };
        }

        public UserControl GetVisualisationUserControl(IConfigurable configurable)
        {
            _delayTime = configurable.NextMoveDelay;
            return new DynaBlasterUserControl(_arena ?? (_arena = new GameArena()));
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            Reset();

            for (var i = 0; i < _arena.Board.GetLength(0); i++)
            {
                for (var j = 0; j < _arena.Board.GetLength(1); j++)
                {
                    var tileType = _rand.Next(2) == 0 ? BoardTile.Empty : (BoardTile) (_rand.Next(3) + 1);
                    _arena.Board[i, j] = tileType;
                }
            }

            _arena.Bots = competitors.Select(competitor => new DynaBlasterBot(competitor)).ToList();
            _arena.Bots.ForEach(bot =>
            {
                bot.Color = Color.FromRgb((byte) _rand.Next(256), (byte) _rand.Next(256), (byte) _rand.Next(256));
                bot.Location = GetRandomEmptyPointOnBoard();
            });

            _arena.OnArenaChanged();
        }

        public void Reset()
        {
            _arena.Bots.Clear();
            _arena.Bombs.Clear();
            _arena.Explosions.Clear();
            _arena.Board = new BoardTile[15, 15];
            _arena.OnArenaChanged();
            _roundNumber = 0;
        }

        public void SetPreview(object boardState)
        {
            //TODO: implement
            _arena.OnArenaChanged();
        }

        public string GetGameRules()
        {
            return Properties.Resources.GameRules;
        }

        #endregion

        #region Private methods

        private Point GetRandomEmptyPointOnBoard()
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

        private bool IsMoveValid(DynaBlasterBot bot, BotMove move)
        {
            var newLocation = GetNewLocation(bot.Location, move.Direction);
            return IsLocationValid(newLocation) && _arena.Board[newLocation.X, newLocation.Y] == BoardTile.Empty
                   && !_arena.Bots.Any(blasterBot => blasterBot.Id != bot.Id && blasterBot.Location == newLocation);
        }

        private void PerformMove(DynaBlasterBot bot, BotMove move)
        {
            if (move.Action == BotAction.DropBomb)
            {
                _arena.Bombs.Add(new Bomb()
                {
                    Location = bot.Location,
                    RoundsUntilExplodes = 5,
                    ExplosionRadius = _explosionRadius
                });
            }
            bot.Location = GetNewLocation(bot.Location, move.Direction);

        }

        private Point GetNewLocation(Point oldLocation, MoveDirection? direction)
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

        private bool IsLocationValid(Point location)
        {
            return location.X >= 0
                && location.X < _arena.Board.GetLength(0)
                && location.Y >= 0
                && location.Y < _arena.Board.GetLength(1);
        }

        private void SetExplosion(ExplodableBase explodable)
        {
            var explosion = new Explosion()
            {
                Center = explodable.Location,
                BlastLocations = GetExplosionLocations(explodable)
            };

            _arena.Explosions.Add(explosion);

            explodable.IsExploded = true;

            foreach (var explosionLocation in explosion.BlastLocations)
            {
                switch (_arena.Board[explosionLocation.X, explosionLocation.Y])
                {
                    case BoardTile.Regular:
                        _arena.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Empty;
                        break;
                    case BoardTile.Fortified:
                        _arena.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Regular;
                        break;
                }

                _arena.Bots.ForEach(bot =>
                {
                    if (bot.Location == explosionLocation)
                    {
                        bot.IsDead = true;
                    }
                });
            }

            SetChainedExplosions(explosion);
        }


        private void SetChainedExplosions(Explosion explosion)
        {
            foreach (var bomb in _arena.Bombs)
            {
                if (explosion.BlastLocations.Any(point => point == bomb.Location) && !bomb.IsExploded)
                {
                    SetExplosion(bomb);
                }
            }

            foreach (var missile in _arena.Missiles)
            {
                if (explosion.BlastLocations.Any(point => point == missile.Location) && !missile.IsExploded)
                {
                    SetExplosion(missile);
                }
            }
        }

        private IEnumerable<Point> GetExplosionLocations(ExplodableBase explodable)
        {
            var result = new List<Point> {explodable.Location};

            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Up));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Down));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Right));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Left));

            return result;
        }

        private IEnumerable<Point> CalculateExplosionRay(ExplodableBase explodable, MoveDirection direction)
        {
            var currentPoint = explodable.Location;
            for (int i = 1; i <= explodable.ExplosionRadius; i++)
            {
                currentPoint = GetNewLocation(currentPoint, direction);

                if (!IsLocationValid(currentPoint))
                {
                    yield break;
                }

                yield return currentPoint;

                if (_arena.Board[currentPoint.X, currentPoint.Y] != BoardTile.Empty)
                {
                    yield break;
                }
            }
        } 

        private BotArenaInfo GetBotArenaInfo(DynaBlasterBot bot)
        {
            return new BotArenaInfo()
            {
                BotId = bot.Id,
                Board = _arena.Board,
                Bombs = _arena.Bombs,
                BotLocation = bot.Location,
                OpponentLocations = _arena.Bots.Where(blasterBot => blasterBot.Id != bot.Id && !blasterBot.IsDead).Select(blasterBot => blasterBot.Location).ToList()
            };
        }

        #endregion
    }
}