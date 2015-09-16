using System.Windows;
using System.Windows.Controls;
using Arena.ViewModels;
using Common.Models;

namespace Arena.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel _viewModel
        {
            get { return (MainWindowViewModel)DataContext; }
            set { DataContext = value; }
        }

        public MainWindow(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        private void GameHistoryTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var roundPartial = ((TreeView)sender).SelectedItem as RoundPartialHistory;
            if (roundPartial != null)
            {
                _viewModel.Game.SetPreview(roundPartial.BoardState);
            }
        }

        private void OutputWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            OutputWindow.ScrollToEnd();
        }
    }
}
