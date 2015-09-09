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
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.UserControls
{
    /// <summary>
    /// Interaction logic for DynaBlasterUserControl.xaml
    /// </summary>
    public partial class DynaBlasterUserControl : UserControl
    {
        public DynaBlasterUserControl(DynaBlasterControlViewModel viewModel)
        {
            
            InitializeComponent();
            DataContext = viewModel;
            for (int i = 0; i < viewModel.Board.XSize; i++)
            {
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < viewModel.Board.YSize; i++)
            {
                BoardGrid.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
