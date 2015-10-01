using Game.AntWars.ViewModels;

namespace Game.AntWars.UserControls
{
    public partial class AntWarsUserControl 
    {
        public AntWarsUserControl(AntWarsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
