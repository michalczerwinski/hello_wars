using System.Collections.Generic;
using System.Drawing;

namespace Game.TankBlaster.Models
{
    public class Explosion
    {
        public Point Center { get; set; }
        public IEnumerable<Point> BlastLocations { get; set; }
    }
}
