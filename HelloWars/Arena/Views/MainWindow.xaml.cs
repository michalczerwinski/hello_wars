using Arena.ViewModels;

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
    }
}
