using System.Drawing;
using Game.TankBlaster.Models;

namespace Game.TankBlaster.Interfaces
{
    public interface IMissile
    {
        Point Location { get; set; }
        int ExplosionRadius { get; set; }
        MoveDirection MoveDirection { get; set; }
    }
}
