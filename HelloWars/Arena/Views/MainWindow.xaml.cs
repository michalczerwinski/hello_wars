using System.Windows;
using System.Windows.Controls;
using Arena.ViewModels;

namespace Arena.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = (MainWindowViewModel)DataContext;
            EliminationTypeGrid.Children.Add(_viewModel.EliminationTypeControl);
            _viewModel.GameControlChanged += GameControlChanged;
        }

        public void GameControlChanged(object sender, GameControlChangedEventArgs eventArgs)
        {
            GameTypeGrid.Children.Clear();
            GameTypeGrid.Children.Add(eventArgs.NewControl);
        }
    }
}
