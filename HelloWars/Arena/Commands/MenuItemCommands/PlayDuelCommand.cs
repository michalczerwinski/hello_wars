using System.Collections.Generic;
using System.Threading.Tasks;
using Arena.ViewModels;
using Common;
using Common.Models;

namespace Arena.Commands.MenuItemCommands
{
    public class PlayDuelCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public PlayDuelCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public async override void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = true;
            await _viewModel.PlayNextGameAsync();
            _viewModel.IsGameInProgress = false;
        }
    }
}