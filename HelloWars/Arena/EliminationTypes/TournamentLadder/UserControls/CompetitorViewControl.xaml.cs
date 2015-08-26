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
        public int Id;
        public int PairWithId;
        public int NextStageTargetId;
        public Point CompetitorHeadPoint;
        public Point CompetitorTailPoint;

        public CompetitorControlViewModel ViewModel
        {
            get { return (CompetitorControlViewModel)DataContext; }
            set { DataContext = value; }
        }

        public CompetitorViewControl(CompetitorControlViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
