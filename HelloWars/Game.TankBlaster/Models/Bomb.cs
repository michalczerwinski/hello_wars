using Game.TankBlaster.Interfaces;

namespace Game.TankBlaster.Models
{
    public class Bomb : ExplodableBase, IBomb
    {
        public Bomb()
        {}

        public Bomb(Bomb bomb) : base(bomb)
        {
            RoundsUntilExplodes = bomb.RoundsUntilExplodes;
        }

        public int RoundsUntilExplodes { get; set; }
    }
}