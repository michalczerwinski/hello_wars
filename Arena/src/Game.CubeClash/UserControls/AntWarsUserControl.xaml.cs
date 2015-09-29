using Game.AntWars.ViewModels;

namespace Game.AntWars.UserControls
{
    public partial class AntWarsUserControl 
    {
        private AntWarsViewModel _viewModel
        {
            get { return (AntWarsViewModel) DataContext; }
            set { DataContext = value; }
        }

        public AntWarsUserControl(AntWarsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
    }
}
