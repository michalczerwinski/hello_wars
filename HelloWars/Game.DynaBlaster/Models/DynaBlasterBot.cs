using System;
using System.Drawing;
using System.Windows.Media;
using Common.Interfaces;
using Common.Models;
using Color = System.Windows.Media.Color;

namespace Game.DynaBlaster.Models
{
    public class DynaBlasterBot : BotClientBase<GameState, BotMove>
    {
        public Color Color { get; set; }
        public Point Location { get; set; }
        public bool IsDead { get; set; }

        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public event EventHandler BotUpdated;

        protected void OnBotUpdated()
        {
            if (BotUpdated != null)
            {
                BotUpdated(this, EventArgs.Empty);
            }
        }

        public DynaBlasterBot(ICompetitor competitor) : base(competitor)
        {}

        public override BotMove NextMove(GameState arenaInfo)
        {
            return new BotMove()
            {
                Direction = (MoveDirection)_rand.Next(4),
                ShouldDropBomb = _rand.Next(7) == 0
            };
        }
    }
}