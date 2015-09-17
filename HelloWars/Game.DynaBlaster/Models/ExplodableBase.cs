using System.Drawing;

namespace Game.DynaBlaster.Models
{
    public class ExplodableBase
    {
        public Point Location { get; set; }
        public int ExplosionRadius { get; set; }
        public bool IsExploded { get; set; }
    }
}
