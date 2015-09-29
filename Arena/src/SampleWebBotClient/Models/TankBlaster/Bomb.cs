using System.Drawing;

namespace SampleWebBotClient.Models.TankBlaster
{
    public class Bomb
    {
        public Point Location { get; set; }
        public int RoundsUntilExplodes { get; set; }
        public int ExplosionRadius { get; set; }
    }
}
