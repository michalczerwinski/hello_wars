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
using System.Windows.Shapes;
using Arena.ViewModels;
using Common.Interfaces;

namespace Arena.Views
{
    /// <summary>
    /// Interaction logic for WinnerSelectionWindow.xaml
    /// </summary>
    public partial class WinnerSelectionWindow : Window
    {
        readonly WinnerSelectionViewModel _viewModel;

        public ICompetitor SelectedWinner => _viewModel.SelectedWinner;

        public WinnerSelectionWindow(IEnumerable<ICompetitor> competitors)
        {
            InitializeComponent();
            _viewModel = new WinnerSelectionViewModel(competitors);
            DataContext = _viewModel;
        }
    }
}
