namespace Game.TankBlaster.Models
{
    public class BotMove
    {
        public MoveDirection? Direction { get; set; }
        public BotAction Action { get; set; }
        public MoveDirection FireDirection { get; set; }
    }
}