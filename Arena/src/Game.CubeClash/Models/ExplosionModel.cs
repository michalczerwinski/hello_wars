using Game.AntWars.Enums;
using Game.AntWars.Interfaces;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Models
{
    public class ExplosionModel: IMovableObject
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

        public MovableObjectsTypes Type { get; set; }

        public ExplosionModel(ExplosionViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ExplosionModel(int x, int y)
        {
            ViewModel = new ExplosionViewModel();
            X = x;
            Y = y;
            Type = MovableObjectsTypes.Explosion;
        }
    }
}
