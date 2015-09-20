using Game.CubeClash.Enums;

namespace Game.CubeClash.Models
{
    public class CubeMove
    {
        public AvailableActions AvailableActions { get; set; }
        public ActionDirections ActionDirections { get; set; }
    }
}