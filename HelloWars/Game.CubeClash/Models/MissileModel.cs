using System.Windows.Media;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Models
{
    public class MissileModel : IMovableObject
    {
        public MissileViewModel ViewModel { get; set; }
        private ActionDirections _direction;
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
        }
            }
}
