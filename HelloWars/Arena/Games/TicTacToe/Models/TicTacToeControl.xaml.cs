using System.Windows.Controls;
using Arena.Games.TicTacToe.ViewModels;

namespace Arena.Games.TicTacToe.Models
{
    /// <summary>
    /// Interaction logic for TicTacToeControl.xaml
    /// </summary>
    public partial class TicTacToeControl : UserControl
    {
        public TicTacToeControl(TicTacToeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
