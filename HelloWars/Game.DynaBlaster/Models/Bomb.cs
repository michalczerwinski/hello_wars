using System.Drawing;

namespace Game.DynaBlaster.Models
{
    public class Bomb
    {
        public Point Location { get; set; }
        public int RoundsUntilExplodes { get; set; }
    }
}