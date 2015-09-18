using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Serialization;
using Game.DynaBlaster.Helpers;
using Game.DynaBlaster.Interfaces;
using Game.DynaBlaster.Models;
using Game.DynaBlaster.Properties;
using Game.DynaBlaster.UserControls;

namespace Game.DynaBlaster
{
    public class DynaBlaster : IGame
    {
        private int _delayTime;
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private GameArena _arena;
        private int _roundNumber;
        private DynaBlasterConfig _gameConfig;

        public DynaBlaster()
        {
            var xmlStream = new StreamReader("DynaBlaster.config.xml");
            var configurationXml = xmlStream.ReadToEnd();
            ApplyConfiguration(configurationXml);
        }

        #region IGame members

        public RoundResult PerformNextRound()
        {
            _roundNumber++;

            HandleExplodables();

            if (_arena.Bots.Count(bot => !bot.IsDead) < 2)
            {
                return new RoundResult
                {
                    FinalResult = _arena.Bots.ToDictionary(competitor => competitor as ICompetitor, competitor => competitor.IsDead ? 0.0 : 1.0),
                    IsFinished = true,
                    History = new List<RoundPartialHistory>()
                };
            }

            var partialResults = PlayBotMoves();

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = partialResults.ToList()
            };
        }

        public UserControl GetVisualisationUserControl(IConfigurable configurable)
        {
            _delayTime = configurable.NextMoveDelay;
            return new DynaBlasterUserControl(_arena);
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            Reset();

            for (var i = 0; i < _arena.Board.GetLength(0); i++)
            {
                for (var j = 0; j < _arena.Board.GetLength(1); j++)
                {
                    var tileType = _rand.Next(3) != 0 ? BoardTile.Empty : (BoardTile) (_rand.Next(3) + 1);
                    _arena.Board[i, j] = tileType;
                }
            }

            _arena.Bots = competitors.Select(competitor => new DynaBlasterBot(competitor)).ToList();

            for (var i = 0; i < _arena.Bots.Count; i++)
            {
                _arena.Bots[i].Location = GetRandomEmptyPointOnBoard();
                _arena.Bots[i].Image = ResourceImageHelper.LoadImage(i%2 == 0 ? Resources.tank1 : Resources.tank2);
            }

            _arena.OnArenaChanged();
        }

        public void Reset()
        {
            _arena.Reset();
            _roundNumber = 0;
        }

        public void SetPreview(object boardState)
        {
            _arena.ImportState((GameArena)boardState);
            _arena.OnArenaChanged();
        }

        public string GetGameRules()
        {
            return Resources.GameRules;
        }

        public void ApplyConfiguration(string configurationXml)
        {
            _gameConfig = new XmlSerializer<DynaBlasterConfig>().Deserialize(configurationXml);
            _arena = new GameArena(_gameConfig.MapWidth, _gameConfig.MapHeight);
        }

        #endregion

        #region Private methods

        #region Location methods

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

        private bool IsLocationAvailableForMissile(Point location)
        {
            return IsLocationValid(location) && _arena.Board[location.X, location.Y] == BoardTile.Empty && _arena.Bots.All(blasterBot => blasterBot.Location != location) &&
                   _arena.Bombs.All(bomb => bomb.Location != location) && _arena.Missiles.All(m => m.Location != location);
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

        #endregion

        #region Next round methods

        private void HandleExplodables()
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

            foreach (var missile in _arena.Missiles)
            {
                if (!missile.IsExploded)
                {
                    var newLocation = GetNewLocation(missile.Location, missile.MoveDirection);

                    if (IsLocationAvailableForMissile(newLocation) && _arena.Bots.All(bot => bot.Location != missile.Location))
                    {
                        missile.Location = newLocation;
                    }
                    else
                    {
                        SetExplosion(missile);
                    }
                }
            }

            _arena.Bombs.RemoveAll(bomb => bomb.IsExploded);
            _arena.Missiles.RemoveAll(missile => missile.IsExploded);

            _arena.OnArenaChanged();

            DelayHelper.Delay(_delayTime);

            _arena.Explosions.Clear();
        }

        private IEnumerable<RoundPartialHistory> PlayBotMoves()
        {
            foreach (var dynaBlasterBot in _arena.Bots.Where(bot => !bot.IsDead))
            {
                var move = dynaBlasterBot.NextMove(GetBotArenaInfo(dynaBlasterBot));

                if (IsMoveValid(dynaBlasterBot, move))
                {
                    yield return PerformMove(dynaBlasterBot, move);
                    _arena.OnArenaChanged();
                }

                DelayHelper.Delay(_delayTime);
            }
        }

        private bool IsMoveValid(DynaBlasterBot bot, BotMove move)
        {
            var newLocation = GetNewLocation(bot.Location, move.Direction);
            return IsLocationValid(newLocation) && _arena.Board[newLocation.X, newLocation.Y] == BoardTile.Empty
                   && !_arena.Bots.Any(blasterBot => blasterBot.Id != bot.Id && blasterBot.Location == newLocation);
        }

        private RoundPartialHistory PerformMove(DynaBlasterBot bot, BotMove move)
        {
            var actionDescription = move.Direction != null ? "move " + move.Direction.Value : "stay";

            if (move.Action == BotAction.DropBomb)
            {
                _arena.Bombs.Add(new Bomb
                {
                    Location = bot.Location,
                    RoundsUntilExplodes = 5,
                    ExplosionRadius = _gameConfig.BombBlastRadius
                });

                actionDescription += " & drop bomb";
            }

            bot.Location = GetNewLocation(bot.Location, move.Direction);
            bot.LastDirection = move.Direction ?? bot.LastDirection;

            if (move.Action == BotAction.FireMissile)
            {
                if (IsMissileAvailable(bot) && IsLocationAvailableForMissile(GetNewLocation(bot.Location, move.FireDirection)))
                {
                    bot.LastMissileFiredRound = _roundNumber;
                    _arena.Missiles.Add(new Missile
                    {
                        ExplosionRadius = _gameConfig.MissileBlastRadius,
                        MoveDirection = move.FireDirection,
                        Location = GetNewLocation(bot.Location, move.FireDirection)
                    });
                    actionDescription += " & fire " + move.FireDirection;
                }
                else
                {
                    actionDescription += " & can't fire " + move.FireDirection;
                }
            }

            return new RoundPartialHistory()
            {
                Caption = string.Format("Round {0} {1}: {2}", _roundNumber, bot.Name, actionDescription),
                BoardState = _arena.ExportState()
            };
        }

        private bool IsMissileAvailable(DynaBlasterBot bot)
        {
            return _roundNumber - _gameConfig.RoundsBetweenMissiles > bot.LastMissileFiredRound;
        }

        private BotArenaInfo GetBotArenaInfo(DynaBlasterBot bot)
        {
            return new BotArenaInfo
            {
                BotId = bot.Id,
                Board = _arena.Board,
                Bombs = _arena.Bombs.Cast<IBomb>().ToList(),
                BotLocation = bot.Location,
                OpponentLocations = _arena.Bots.Where(blasterBot => blasterBot.Id != bot.Id && !blasterBot.IsDead).Select(blasterBot => blasterBot.Location).ToList(),
                Missiles = _arena.Missiles.Cast<IMissile>().ToList(),
                IsMissileAvailable = IsMissileAvailable(bot)
            };
        }

        #endregion

        #region Explosion methods

        private void SetExplosion(ExplodableBase explodable)
        {
            var explosion = new Explosion
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
            for (var i = 1; i <= explodable.ExplosionRadius; i++)
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

        #endregion

        #region Configuration methods


        #endregion

        #endregion
    }
}