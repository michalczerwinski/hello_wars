using System.Windows.Controls;

namespace Arena.Games.Tanks
{
    /// <summary>
    /// Interaction logic for TankGameControl.xaml
    /// </summary>
    public partial class TankGameControl : UserControl
    {
        private TankGameViewModel _viewModel;

        public TankGameControl(TankGameViewModel viewModel)
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
