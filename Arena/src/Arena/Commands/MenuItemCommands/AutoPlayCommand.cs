using Arena.ViewModels;
using Common.Helpers;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    public class AutoPlayCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public AutoPlayCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = true;

            if (_viewModel.IsGamePaused)
            {
                _viewModel.IsGamePaused = false;
                await _viewModel.ResumeGameAsync();
                await DelayHelper.DelayAsync(_viewModel.ArenaConfiguration.GameConfiguration.NextMatchDelay);
            }

            while (_viewModel.Elimination.GetNextCompetitors() != null && _viewModel.IsGameInProgress)
            {
                await _viewModel.PlayNextGameAsync();
                await DelayHelper.DelayAsync(_viewModel.ArenaConfiguration.GameConfiguration.NextMatchDelay);
            }
            _viewModel.IsGameInProgress = false;

            if (_viewModel.ShouldRestartGame)
            {
                _viewModel.RestartGame();
            }
        }
    }
}
