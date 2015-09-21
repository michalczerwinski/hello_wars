using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Models
{
    class GridUnitModel: IUnmovableObject
    {
        public GridUnitViewModel ViewModel { get; set; }

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

        public GridUnitModel(GridUnitViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public UnmovableObjectTypes Type
        {
            get { return ViewModel.LandType; }
            set { ViewModel.LandType = value; }
        }
    }
}
