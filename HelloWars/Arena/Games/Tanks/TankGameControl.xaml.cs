using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
