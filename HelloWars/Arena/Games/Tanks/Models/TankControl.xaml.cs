using Arena.Games.Tanks.ViewModels;

namespace Arena.Games.Tanks.Models
{
    /// <summary>
    /// Interaction logic for TankControl.xaml
    /// </summary>
    public partial class TankControl 
    {
        public TankControl(TankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
