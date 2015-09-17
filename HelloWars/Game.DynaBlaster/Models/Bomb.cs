using System.Drawing;
using Game.DynaBlaster.Interfaces;

namespace Game.DynaBlaster.Models
{
    public class Bomb : ExplodableBase, IBomb
    {
        public int RoundsUntilExplodes { get; set; }
    }
}