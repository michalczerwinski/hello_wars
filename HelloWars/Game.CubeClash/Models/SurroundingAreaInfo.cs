using Game.AntWars.Enums;

namespace Game.AntWars.Models
{
    public class SurroundingAreaInfo
    {
        public AllGameObjectTypes[,] Objects { get; set; }

        public SurroundingAreaInfo(int rangeX, int rangeY)
        {
            Objects = new AllGameObjectTypes[rangeX, rangeY];
        }
    }
}
