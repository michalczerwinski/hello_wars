using Game.DynaBlaster.Interfaces;

namespace Game.DynaBlaster.Models
{
    public class Missile : ExplodableBase, IMissile
    {
        public Missile()
        {}

        public Missile(Missile missile) : base(missile)
        {
            MoveDirection = missile.MoveDirection;
        }

        public MoveDirection MoveDirection { get; set; }
    }
}
