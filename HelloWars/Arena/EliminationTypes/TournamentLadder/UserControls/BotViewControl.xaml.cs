using System.Windows;
using System.Windows.Controls;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    /// <summary>
    /// Interaction logic for UserViewControl.xaml
    /// </summary>
    /// []
    public partial class BotViewControl : UserControl
    {
        public int Id;
        public int PairWithId;
        public int NextStageTargetId;
        public Point BotHeadPoint;
        public Point BotTailPoint;

        public BotControlViewModel ViewModel
        {
            get { return (BotControlViewModel)DataContext; }
            set { DataContext = value; }
        }

        public BotViewControl(BotControlViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
