using Game.AntWars.Enums;
using Game.AntWars.Interfaces;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Models
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
