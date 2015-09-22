using Game.AntWars.Enums;

namespace Game.AntWars.Interfaces
{
    public interface IUnmovableObject
    {
        int X { get; set; }
        int Y { get; set; }
        UnmovableObjectTypes Type { get; }
    } 
}
