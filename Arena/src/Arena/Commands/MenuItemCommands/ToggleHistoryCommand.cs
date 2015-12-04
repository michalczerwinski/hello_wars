using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class ToggleHistoryCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public ToggleHistoryCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            if (!_viewModel.IsHistoryVisible)
            {
                _viewModel.SelectedTabIndex = 0;
            }
        }
    }
}
