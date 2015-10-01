using Elimination.RoundRobin.ViewModels;

namespace Elimination.RoundRobin.UserControls
{
    public partial class RoundRobinUserControl
    {
        public RoundRobinUserControl(RoundRobinViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
