using Arena.ViewModels;

namespace Arena.Commands.MenuItemCommands
{
    class ToggleHistoryCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public ToggleHistoryCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            base.Execute(parameter);
        }
    }
}
