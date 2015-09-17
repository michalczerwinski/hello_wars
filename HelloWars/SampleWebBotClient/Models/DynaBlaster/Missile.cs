using System.Drawing;

namespace SampleWebBotClient.Models.DynaBlaster
{
    public class Missile
    {
        public Point Location { get; set; }
        public int ExplosionRadius { get; set; }
        public MoveDirection MoveDirection { get; set; }
    }
}
