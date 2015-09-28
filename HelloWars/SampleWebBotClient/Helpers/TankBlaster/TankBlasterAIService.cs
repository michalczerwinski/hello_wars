using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Helpers.TankBlaster
{
    public class TankBlasterAiService
    {
        private BotArenaInfo _arena;
        
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);
        
        private LocationService _locationService;
        private CombatService _combatService;

        public BotMove CalculateNextMove(BotArenaInfo arena)
        {
            _arena = arena;
            _locationService = new LocationService(arena);
            _combatService = new CombatService(_arena, _locationService);

            return GenerateNextMove();
        }

        private BotMove GenerateNextMove()
        {
            var result = new BotMove()
            {
                Action = _rand.Next(7) == 0 ? BotAction.DropBomb : BotAction.None
            };

            var safestDirections = _combatService.SafestDirections(_arena.BotLocation).ToList();

            var currentObjective = _combatService.GetCurrentObjective();
            List<MoveDirection> objectiveDirections = new List<MoveDirection>();
            switch (currentObjective)
            {
                case Objective.GetCloserToOpponent:
                    objectiveDirections = _locationService.MatchingDirections(_arena.BotLocation, _combatService.ClosestOpponentLocation);
                    break;
                case Objective.GetIntoFiringPosition:
                    objectiveDirections = _locationService.AllMoveDirections;
                    break;
                case Objective.DestroyThatSucker:
                    if (_combatService.IsSafe(_arena.BotLocation))
                    {
                        result.Action = BotAction.FireMissile;
                        result.FireDirection = _locationService.MatchingDirections(_arena.BotLocation, _combatService.ClosestOpponentLocation).First();
                        result.Direction = null;
                        return result;
                    }
                    break;
            }
            result.Direction = GetDirection(safestDirections, objectiveDirections);
            return result;
        }

        private MoveDirection? GetDirection(List<MoveDirection> safestDirections, List<MoveDirection> objectiveDirections)
        {
            var viableDirections = safestDirections.Intersect(objectiveDirections).ToList();

            if (viableDirections.Any())
            {
                return viableDirections[_rand.Next(viableDirections.Count)];
            }
            else
            {
                return safestDirections.Any() ? safestDirections[_rand.Next(safestDirections.Count)] : (MoveDirection?) null;
            }
        }
    }
}