using Game.DynaBlaster.Interfaces;

namespace Game.DynaBlaster.Models
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