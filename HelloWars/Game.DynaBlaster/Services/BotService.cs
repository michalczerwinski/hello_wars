using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.DynaBlaster.Interfaces;
using Game.DynaBlaster.Models;
using Game.DynaBlaster.Properties;

namespace Game.DynaBlaster.Services
{
    internal class BotService
    {
        private readonly Battlefield _field;
        private readonly DynaBlasterConfig _gameConfig;
        private readonly LocationService _locationService;

        public BotService(Battlefield field, DynaBlasterConfig config, LocationService locationService)
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

        public async Task<List<RoundPartialHistory>> PlayBotMovesAsync(int delayTime, int roundNumber)
        {
            var result = new List<RoundPartialHistory>();
            foreach (var dynaBlasterBot in _field.Bots.Where(bot => !bot.IsDead))
            {
                var move = await dynaBlasterBot.NextMoveAsync(GetBotBattlefieldInfo(dynaBlasterBot, roundNumber));

                if (IsMoveValid(dynaBlasterBot, move))
                {
                    result.Add(PerformMove(dynaBlasterBot, move, roundNumber));
                    _field.OnArenaChanged();
                }

                await DelayHelper.DelayAsync(delayTime);
            }

            return result;
        }

        public void SetUpBots(IEnumerable<ICompetitor> competitors)
        {
            _field.Bots = competitors.Select(competitor => new DynaBlasterBot(competitor)).ToList();

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

        private BotBattlefieldInfo GetBotBattlefieldInfo(DynaBlasterBot bot, int roundNumber)
        {
            return new BotBattlefieldInfo
            {
                RoundNumber = roundNumber,
                BotId = bot.Id,
                Board = _field.Board,
                Bombs = _field.Bombs.Cast<IBomb>().ToList(),
                BotLocation = bot.Location,
                OpponentLocations = _field.Bots.Where(blasterBot => blasterBot.Id != bot.Id && !blasterBot.IsDead).Select(blasterBot => blasterBot.Location).ToList(),
                Missiles = _field.Missiles.Cast<IMissile>().ToList(),
                IsMissileAvailable = IsMissileAvailable(bot, roundNumber),
                GameConfig = _gameConfig
            };
        }

        private bool IsMissileAvailable(DynaBlasterBot bot, int roundNumber)
        {
            return roundNumber - _gameConfig.RoundsBetweenMissiles > bot.LastMissileFiredRound;
        }

        private bool IsMoveValid(DynaBlasterBot bot, BotMove move)
        {
            var newLocation = _locationService.GetNewLocation(bot.Location, move.Direction);
            return _locationService.IsLocationValid(newLocation) && _field.Board[newLocation.X, newLocation.Y] == BoardTile.Empty
                   && !_field.Bots.Any(blasterBot => blasterBot.Id != bot.Id && blasterBot.Location == newLocation);
        }

        private RoundPartialHistory PerformMove(DynaBlasterBot bot, BotMove move, int roundNumber)
        {
            var actionDescription = move.Direction != null ? "move " + move.Direction.Value : "stay";

            if (move.Action == BotAction.DropBomb)
            {
                _field.Bombs.Add(new Bomb
                {
                    Location = bot.Location,
                    RoundsUntilExplodes = 5,
                    ExplosionRadius = CurrentBombBlastRadius(roundNumber)
                });

                actionDescription += " & drop bomb";
            }

            bot.Location = _locationService.GetNewLocation(bot.Location, move.Direction);
            bot.LastDirection = move.Direction ?? bot.LastDirection;

            if (move.Action == BotAction.FireMissile)
            {
                if (IsMissileAvailable(bot, roundNumber) && _locationService.IsLocationAvailableForMissile(_locationService.GetNewLocation(bot.Location, move.FireDirection)))
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