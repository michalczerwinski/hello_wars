using Game.CubeClash.ViewModels;

namespace Game.CubeClash.UserControls
{
    /// <summary>
    /// Interaction logic for CubeClashUserControl.xaml
    /// </summary>
    public partial class CubeClashUserControl 
    {
        private CubeClashViewModel _viewModel
        {
            get { return (CubeClashViewModel) DataContext; }
            set { DataContext = value; }
        }

        public CubeClashUserControl(CubeClashViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
    }
}
