using System.Windows.Controls;
using Arena.Games.TicTacToe.ViewModels;

namespace Arena.Games.TicTacToe.UserControls
{
    /// <summary>
    /// Interaction logic for TicTacToeControl.xaml
    /// </summary>
    public partial class TicTacToeUserControl : UserControl
    {
        public TicTacToeUserControl(TicTacToeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
