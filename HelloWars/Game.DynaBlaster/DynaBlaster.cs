using System;
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
            _arena.ExplosionCenters.Clear();

            foreach (var bomb in _arena.Bombs)
            {
                bomb.RoundsUntilExplodes--;
                if (bomb.RoundsUntilExplodes == 0)
                {
                    SetExplosion(bomb.Location);
                }
            }

            _arena.Bombs.RemoveAll(bomb => bomb.RoundsUntilExplodes == 0);

            _arena.OnArenaChanged();

            DelayHelper.Delay(_delayTime);

            _arena.ExplosionCenters.Clear();

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
                    _arena.Board[i, j] = _rand.Next() % 2 == 0;
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
            _arena.ExplosionCenters.Clear();
            _arena.Board = new bool[15, 15];
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

                if (!_arena.Board[result.X, result.Y] && _arena.Bots.All(bot => bot.Location != result))
                {
                    return result;
                }
            }
        }

        private bool IsMoveValid(DynaBlasterBot bot, BotMove move)
        {
            var newLocation = GetNewLocation(bot.Location, move.Direction);
            return IsLocationValid(newLocation) && !_arena.Board[newLocation.X, newLocation.Y]
                   && !_arena.Bots.Any(blasterBot => blasterBot.Id != bot.Id && blasterBot.Location == newLocation);
        }

        private void PerformMove(DynaBlasterBot bot, BotMove move)
        {
            if (move.ShouldDropBomb)
            {
                _arena.Bombs.Add(new Bomb()
                {
                    Location = bot.Location,
                    RoundsUntilExplodes = 5
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

        private void SetExplosion(Point location)
        {
            _arena.ExplosionCenters.Add(location);
            
            foreach (var explosionLocation in GetExplosionLocations(location))
            {
                _arena.Board[explosionLocation.X, explosionLocation.Y] = false;
                _arena.Bots.ForEach(bot =>
                {
                    if (bot.Location == explosionLocation)
                    {
                        bot.IsDead = true;
                    }
                });
            }
        }

        private IEnumerable<Point> GetExplosionLocations(Point centerLocation)
        {
            yield return centerLocation;

            for (int i = 1; i <= _explosionRadius; i++)
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

        private BotArenaInfo GetBotArenaInfo(DynaBlasterBot bot)
        {
            return new BotArenaInfo()
            {
                BotId = bot.Id,
                Board = _arena.Board,
                Bombs = _arena.Bombs,
                Explosions = _arena.ExplosionCenters.SelectMany(GetExplosionLocations).ToList(),
                BotLocation = bot.Location,
                OpponentLocations = _arena.Bots.Where(blasterBot => blasterBot.Id != bot.Id && !blasterBot.IsDead).Select(blasterBot => blasterBot.Location).ToList()
            };
        }

        #endregion
    }
}