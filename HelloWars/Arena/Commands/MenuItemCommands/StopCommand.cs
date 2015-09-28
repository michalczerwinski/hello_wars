using Arena.ViewModels;
using Common;

namespace Arena.Commands.MenuItemCommands
{
    public class StopCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public StopCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = false;
        }
    }
}
