using Game.AntWars.Enums;
using Game.AntWars.Interfaces;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Models
{
    public class MissileModel : IMovableObject
    {
        public MissileViewModel ViewModel { get; set; }
        public int Range;

        public ActionDirections Direction
        {
            get { return ViewModel.Direction; }
            set { ViewModel.Direction = value; }
        }

        public int X
        {
            get { return ViewModel.X; }
            set { ViewModel.X = value; }
        }

        public int Y
        {
            get { return ViewModel.Y; }
            set { ViewModel.Y = value; }
        }

        public MissileModel(MissileViewModel viewModel)
        {
            ViewModel = viewModel;
            Type = MovableObjectsTypes.Missile;
        }

        public MovableObjectsTypes Type { get; set; }
    }
}
