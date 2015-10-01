using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    public class PauseCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public PauseCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.IsGamePaused = true;
            _viewModel.IsGameInProgress = false;    
        }
    }
}
