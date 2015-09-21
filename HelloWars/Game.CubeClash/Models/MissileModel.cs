using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Models
{
    public class MissileModel : IMovableObject
    {
        private MissileViewModel _viewModel;
        private AvailableMoves _direction;
        public int Range;

        public AvailableMoves Direction
        {
            get { return _viewModel.Direction; }
            set { _viewModel.Direction = value; }
        }

        public int X
        {
            get { return _viewModel.X; }
            set { _viewModel.X = value; }
        }

        public int Y
        {
            get { return _viewModel.Y; }
            set { _viewModel.Y = value; }
        }

        public MissileModel(MissileViewModel viewModel)
        {
            _viewModel = viewModel;
            Range = 10;
        }
    }
}
