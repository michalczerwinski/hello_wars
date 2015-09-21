using System.Drawing;
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.Interfaces
{
    public interface IMissile
    {
        Point Location { get; set; }
        int ExplosionRadius { get; set; }
        MoveDirection MoveDirection { get; set; }
    }
}
