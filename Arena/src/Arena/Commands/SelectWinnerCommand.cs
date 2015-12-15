using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.ViewModels;
using Arena.Views;
using Common.Utilities;

namespace Arena.Commands
{
    public class SelectWinnerCommand : CommandBase
    {
        readonly WinnerSelectionViewModel _viewModel;

        public SelectWinnerCommand(WinnerSelectionViewModel  viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter = null)
        {
            var wnd = parameter as WinnerSelectionWindow;
            wnd.DialogResult = true;
            wnd.Close();
        }

        public override bool CanExecute(object parameter = null)
        {
            return _viewModel.SelectedWinner != null;
        }
    }
}
