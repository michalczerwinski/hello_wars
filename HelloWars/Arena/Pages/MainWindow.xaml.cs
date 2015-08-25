using System.Windows;
using Arena.ViewModels;

namespace Arena.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            GameTypeGrid.Children.Add(_viewModel.GameTypeControl);
        }
    }
}
