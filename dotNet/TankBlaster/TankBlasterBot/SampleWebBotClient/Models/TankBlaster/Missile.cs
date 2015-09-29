using System.Drawing;

namespace SampleWebBotClient.Models.TankBlaster
{
    public class Missile
    {
        public Point Location { get; set; }
        public int ExplosionRadius { get; set; }
        public MoveDirection MoveDirection { get; set; }
    }
}
