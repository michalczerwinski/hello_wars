using Game.TicTacToe.ViewModels;

namespace Game.TicTacToe.UserControls
{
    /// <summary>
    /// Interaction logic for TicTacToeControl.xaml
    /// </summary>
    public partial class TicTacToeUserControl 
    {
        public TicTacToeUserControl(TicTacToeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
