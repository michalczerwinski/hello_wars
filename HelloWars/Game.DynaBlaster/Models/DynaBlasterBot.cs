using System.Drawing;
using Common.Interfaces;
using Common.Models;
using Color = System.Windows.Media.Color;

namespace Game.DynaBlaster.Models
{
    public class DynaBlasterBot : BotClientBase<BotArenaInfo, BotMove>
    {
        public DynaBlasterBot(ICompetitor competitor) : base(competitor)
        {
        }

        public Color Color { get; set; }
        public Point Location { get; set; }
        public bool IsDead { get; set; }
    }
}