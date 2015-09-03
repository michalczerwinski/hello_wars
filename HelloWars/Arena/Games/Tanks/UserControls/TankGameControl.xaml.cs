using System.Windows.Controls;
using Arena.Games.Tanks.ViewModels;

namespace Arena.Games.Tanks.UserControls
{
    /// <summary>
    /// Interaction logic for TankGameControl.xaml
    /// </summary>
    public partial class TankGameUserControl 
    {
        private readonly TankGameViewModel _viewModel;

        public TankGameUserControl(TankGameViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
            SetBattlefieldGrid();
        }

        private void SetBattlefieldGrid()
        {
            for (int i = 0; i < _viewModel.Width / 10; i++)
            {
                BattlefieldGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _viewModel.Heigth / 10; i++)
            {
                BattlefieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}
