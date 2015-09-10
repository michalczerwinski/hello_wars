using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DynaBlaster.Models
{
    public class GameState
    {
        public bool[,] Board { get; set; }
        public List<Point> OponentLocations { get; set; }
        public List<Bomb> Bombs { get; set; } 
    }
}
