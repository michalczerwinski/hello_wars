using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    public class RestartCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public RestartCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            if (_viewModel.IsGameInProgress)
            {
                _viewModel.ShouldRestartGame = true;
                _viewModel.IsGameInProgress = false;
            }
            else
            {
                _viewModel.RestartGame();
            }
            
        }
    }
}
