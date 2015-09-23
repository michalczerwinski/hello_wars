using Game.AntWars.Enums;

namespace Game.AntWars.Models
{
    public class BotMove
    {
        public AvailableActions Action { get; set; }
        public ActionDirections ActionDirection { get; set; }
    }
}