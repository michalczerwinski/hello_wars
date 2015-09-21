using Game.CubeClash.Enums;

namespace Game.CubeClash.Interfaces
{
    public interface IUnmovableObject
    {
        int X { get; set; }
        int Y { get; set; }
        UnmovableObjectTypes Type { get; set; }
    } 
}
