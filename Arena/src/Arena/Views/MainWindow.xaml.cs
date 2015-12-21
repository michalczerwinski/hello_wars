using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.ViewModels;
using Common.Models;

namespace Arena.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)DataContext; }
            set { DataContext = value; }
        }

        public MainWindow(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void GameHistoryTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var roundPartial = ((TreeView)sender).SelectedItem as RoundPartialHistory;
            if (roundPartial != null && !ViewModel.IsGameInProgress)
            {
                ViewModel.IsArenaMessageVisible = false;
                ViewModel.Game.SetPreview(roundPartial.BoardState);
            }
        }

        private void OutputWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            OutputWindow.ScrollToEnd();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ViewModel.IsFullScreenApplied = false;
            }
        }
    }
}
