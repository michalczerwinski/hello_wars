using Game.CubeClash.Enums;

namespace Game.CubeClash.Models
{
    public class CubeMove
    {
        public AvailableActions Action { get; set; }
        public ActionDirections ActionDirection { get; set; }
    }
}