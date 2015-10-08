using Game.AntWars.Enums;

namespace Game.AntWars.Interfaces
{
    public interface IMovableObject
    {
        int X { get; set; }
        int Y { get; set; }
        MovableObjectsTypes Type { get; }
    }
}
