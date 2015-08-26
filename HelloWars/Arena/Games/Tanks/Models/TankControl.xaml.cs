using System.Windows.Controls;
using Arena.Games.Tanks.ViewModels;

namespace Arena.Games.Tanks.Models
{
    /// <summary>
    /// Interaction logic for TankControl.xaml
    /// </summary>
    public partial class TankControl : UserControl
    {
        public TankControl(TankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
