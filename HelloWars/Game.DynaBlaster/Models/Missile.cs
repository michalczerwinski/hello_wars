using Game.DynaBlaster.Interfaces;

namespace Game.DynaBlaster.Models
{
    public class Missile : ExplodableBase, IMissile
    {
        public MoveDirection MoveDirection { get; set; }
    }
}
