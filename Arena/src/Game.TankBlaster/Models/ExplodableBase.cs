using System.Drawing;

namespace Game.TankBlaster.Models
{
    public class ExplodableBase
    {
        public ExplodableBase()
        {}

        public ExplodableBase(ExplodableBase explodable)
        {
            Location = explodable.Location;
            ExplosionRadius = explodable.ExplosionRadius;
            IsExploded = explodable.IsExploded;
        }

        public Point Location { get; set; }
        public int ExplosionRadius { get; set; }
        public bool IsExploded { get; set; }
    }
}
