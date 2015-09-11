using System.Collections.Generic;
using System.Drawing;

namespace Game.DynaBlaster.Models
{
    public class BotArenaInfo
    {
        public bool[,] Board { get; set; }
        public Point BotLocation { get; set; }
        public List<Point> OponentLocations { get; set; }
        public List<Bomb> Bombs { get; set; }
        public List<Point> Explosions { get; set; }
    }
}