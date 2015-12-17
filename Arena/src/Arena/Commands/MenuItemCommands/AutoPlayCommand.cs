using Arena.ViewModels;
using Common.Helpers;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    public class AutoPlayCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public AutoPlayCommand(MainWindowViewModel viewModel)
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

            while (_viewModel.Elimination.GetNextCompetitors() != null && _viewModel.IsGameInProgress)
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
