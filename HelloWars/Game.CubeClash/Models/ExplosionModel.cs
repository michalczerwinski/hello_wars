using Game.AntWars.Enums;
using Game.AntWars.Interfaces;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Models
{
    public class ExplosionModel: IUnmovableObject
    {
        public ExplosionViewModel ViewModel { get; set; }

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

        public UnmovableObjectTypes Type
        {
            get { return UnmovableObjectTypes.Explosion; }
        }

        public ExplosionModel(ExplosionViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
