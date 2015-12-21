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
    /// Interaction logic for WinnerSelectionDialog.xaml
    /// </summary>
    public partial class WinnerSelectionDialog : Window
    {
        readonly WinnerSelectionViewModel _viewModel;

        public ICompetitor SelectedWinner => _viewModel.SelectedWinner;

        public WinnerSelectionDialog(IEnumerable<ICompetitor> competitors)
        {
            InitializeComponent();
            _viewModel = new WinnerSelectionViewModel(competitors);
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
