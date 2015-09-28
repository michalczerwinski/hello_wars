using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
            if (_viewModel.IsGamePaused)
            {
                _viewModel.IsGamePaused = false;
                await _viewModel.ResumeGameAsync();
            }
            else
            {
                await _viewModel.PlayNextGameAsync();
            }
            _viewModel.IsGameInProgress = false;

            if (_viewModel.ShouldRestartGame)
            {
                _viewModel.RestartGame();
            }
        }
    }
}