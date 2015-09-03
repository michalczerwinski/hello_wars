using Arena.Games.Tanks.ViewModels;

namespace Arena.Games.Tanks.UserControls
{
    /// <summary>
    /// Interaction logic for TankControl.xaml
    /// </summary>
    public partial class TankUserControl 
    {
        public TankUserControl(TankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
