using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.TankBlaster.Interfaces;
using Game.TankBlaster.Models;
using Game.TankBlaster.Properties;

namespace Game.TankBlaster.Services
{
    internal class BotService
    {
        private readonly Battlefield _field;
        private readonly TankBlasterConfig _gameConfig;
        private readonly LocationService _locationService;

        public BotService(Battlefield field, TankBlasterConfig config, LocationService locationService)
        {
            _field = field;
            _gameConfig = config;
            _locationService = locationService;
        }

        public bool AreMultipleBotsLeft
        {
            get { return _field.AliveBots.Count >= 2; }
        }

        public Dictionary<ICompetitor, double> GetBotResults()
        {
            return _field.Bots.ToDictionary(bot => bot as ICompetitor, bot => bot.IsDead ? 0.0 : 1.0);
        }

        public async Task<RoundResult> PlayBotMovesAsync(int delayTime, int roundNumber)
        {
            var result = new RoundResult()
            {
                FinalResult = null,
                IsFinished = false,
                History = new List<RoundPartialHistory>()
            };

            int turnNumber = 0;
            foreach (var dynaBlasterBot in _field.Bots.Where(bot => !bot.IsDead))
            {
                BotMove move;
                try
                {
                    turnNumber++;
                    move = await dynaBlasterBot.NextMoveAsync(GetBotBattlefieldInfo(dynaBlasterBot, roundNumber, turnNumber));
                }
                catch (Exception e)
                {
                    move = new BotMove()
                    {
                        Action = BotAction.None,
                        Direction = null
                    };

                    result.OutputText += string.Format("Bot {0} threw exception:\n{1}\n", dynaBlasterBot.Name, e.Message);
                }

                if (IsMoveValid(dynaBlasterBot, move))
                {
                    result.History.Add(PerformMove(dynaBlasterBot, move, roundNumber));
                    _field.OnArenaChanged();
                }

                await DelayHelper.DelayAsync(delayTime);
            }

            return result;
        }

        public void SetUpBots(IEnumerable<ICompetitor> competitors)
        {
            _field.Bots = competitors.Select(competitor => new TankBlasterBot(competitor)).ToList();

            for (var i = 0; i < _field.Bots.Count; i++)
            {
                _field.Bots[i].Location = GetBotRandomStartupLocation();
                _field.Bots[i].Image = ResourceImageHelper.LoadImage(i%2 == 0 ? Resources.tank1 : Resources.tank2);
            }
        }

        private Point GetBotRandomStartupLocation()
        {
            while (true)
            {
                var candidatePoint = _locationService.GetRandomEmptyPointOnBoard();
                if (_locationService.GetAdjacentLocations(candidatePoint).All(point => _locationService.IsLocationEmpty(point)))
                {
                    return candidatePoint;
                }
            }
        }

        private int CurrentBombBlastRadius(int roundNumber)
        {
            return _gameConfig.BombBlastRadius + (_gameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (roundNumber/_gameConfig.RoundsBeforeIncreasingBlastRadius));
        }

        private int CurrentMissileBlastRadius(int roundNumber)
        {
            return _gameConfig.MissileBlastRadius + (_gameConfig.RoundsBeforeIncreasingBlastRadius == 0 ? 0 : (roundNumber/_gameConfig.RoundsBeforeIncreasingBlastRadius));
        }

        private BotBattlefieldInfo GetBotBattlefieldInfo(TankBlasterBot bot, int roundNumber, int turnNumber)
        {
            return new BotBattlefieldInfo
            {
                RoundNumber = roundNumber,
                TurnNumber = turnNumber,
                BotId = bot.Id,
                Board = _field.Board,
                Bombs = _field.Bombs.Cast<IBomb>().ToList(),
                BotLocation = bot.Location,
                OpponentLocations = _field.Bots.Where(blasterBot => blasterBot.Id != bot.Id && !blasterBot.IsDead).Select(blasterBot => blasterBot.Location).ToList(),
                Missiles = _field.Missiles.Cast<IMissile>().ToList(),
                MissileAvailableIn = MissileAvailableIn(bot, roundNumber),
                GameConfig = _gameConfig
            };
        }

        private int MissileAvailableIn(TankBlasterBot bot, int roundNumber)
        {

            int result = _gameConfig.RoundsBetweenMissiles - roundNumber + bot.LastMissileFiredRound;

            return bot.LastMissileFiredRound < 0 || result < 0 ? 0 : result;
        }

        private bool IsMoveValid(TankBlasterBot bot, BotMove move)
        {
            var newLocation = _locationService.GetNewLocation(bot.Location, move.Direction);
            return _locationService.IsLocationValid(newLocation) && _field.Board[newLocation.X, newLocation.Y] == BoardTile.Empty
                   && !_field.Bots.Any(blasterBot => blasterBot.Id != bot.Id && blasterBot.Location == newLocation);
        }

        private RoundPartialHistory PerformMove(TankBlasterBot bot, BotMove move, int roundNumber)
        {
            var actionDescription = move.Direction != null ? "move " + move.Direction.Value : "stay";

            if (move.Action == BotAction.DropBomb)
            {
                _field.Bombs.Add(new Bomb
                {
                    Location = bot.Location,
                    RoundsUntilExplodes = _gameConfig.BombRoundsUntilExplodes,
                    ExplosionRadius = CurrentBombBlastRadius(roundNumber)
                });

                actionDescription += " & drop bomb";
            }

            bot.Location = _locationService.GetNewLocation(bot.Location, move.Direction);
            bot.LastDirection = move.Direction ?? bot.LastDirection;

            if (move.Action == BotAction.FireMissile)
            {
                if (MissileAvailableIn(bot, roundNumber) == 0 && _locationService.IsLocationAvailableForMissile(_locationService.GetNewLocation(bot.Location, move.FireDirection)))
                {
                    bot.LastMissileFiredRound = roundNumber;
                    _field.Missiles.Add(new Missile
                    {
                        ExplosionRadius = CurrentMissileBlastRadius(roundNumber),
                        MoveDirection = move.FireDirection,
                        Location = _locationService.GetNewLocation(bot.Location, move.FireDirection)
                    });
                    actionDescription += " & fire " + move.FireDirection;
                }
                else
                {
                    actionDescription += " & can't fire " + move.FireDirection;
                }
            }

            return new RoundPartialHistory
            {
                Caption = string.Format("Round {0} {1}: {2}", roundNumber, bot.Name, actionDescription),
                BoardState = _field.ExportState()
            };
        }
    }
}