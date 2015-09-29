using Game.TankBlaster.Interfaces;

namespace Game.TankBlaster.Models
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
