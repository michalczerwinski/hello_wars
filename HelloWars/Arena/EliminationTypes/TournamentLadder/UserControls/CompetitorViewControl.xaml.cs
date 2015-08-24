using System.Windows;
using System.Windows.Controls;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    /// <summary>
    /// Interaction logic for UserViewControl.xaml
    /// </summary>
    /// []
    public partial class CompetitorViewControl : UserControl
    {
        public Point CompetitorHeadPoint { get; set; }
        public Point CompetitorTailPoint { get; set; }
     
        public CompetitorViewControl(CompetitorControlViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
