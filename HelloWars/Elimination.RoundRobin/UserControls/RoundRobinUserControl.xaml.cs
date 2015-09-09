using Elimination.RoundRobin.ViewModels;

namespace Elimination.RoundRobin.UserControls
{
    /// <summary>
    /// Interaction logic for RoundRobinUserControl.xaml
    /// </summary>
    public partial class RoundRobinUserControl
    {
        private RoundRobinViewModel _viewModel
        {
            get { return (RoundRobinViewModel)DataContext; }
            set { DataContext = value; }
        }

        public RoundRobinUserControl(RoundRobinViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }
    }
}
