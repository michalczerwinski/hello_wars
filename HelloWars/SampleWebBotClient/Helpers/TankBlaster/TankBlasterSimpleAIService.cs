using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Helpers.TankBlaster
{
    public class TankBlasterSimpleAIService
    {
        private BotArenaInfo _field;

        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public BotMove CalculateNextMove(BotArenaInfo arenaInfo)
        {
            _field = arenaInfo;
            var result = new BotMove()
            {
                Action = _rand.Next(7) == 0 ? BotAction.DropBomb : BotAction.None
            };
        }

        private bool IsInDangerZone(Point location)
        {
            
        }

        private List<Point> GetBombDangerZone(Point centerLocation)
        {
            var result = GetSurroundingPoints(centerLocation, CurrentBombBlastRadius).ToList();
            result.Add(centerLocation);

            return result;
        }

        public IEnumerable<Point> GetSurroundingPoints(Point centerLocation, int radius)
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

                foreach (var point in locations.Where(IsValidLocation))
                {
                    yield return point;
                }
            }
        }

        public bool IsLocationValid(Point location)
        {
            return location.X >= 0
                   && location.X < _field.Board.GetLength(0)
                   && location.Y >= 0
                   && location.Y < _field.Board.GetLength(1);
        }
    }
}