using System.Drawing;

namespace Game.DynaBlaster.Interfaces
{
    public interface IBomb
    {
        Point Location { get; set; }
        int ExplosionRadius { get; set; }
        int RoundsUntilExplodes { get; set; }
    }
}
