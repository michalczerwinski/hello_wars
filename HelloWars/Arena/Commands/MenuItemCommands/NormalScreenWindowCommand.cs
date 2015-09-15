using System.Windows;
using Arena.ViewModels;

namespace Arena.Commands.MenuItemCommands
{
    class NormalScreenWindowCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public NormalScreenWindowCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.WindowState = WindowState.Normal;
            _viewModel.WindowStyle = WindowStyle.SingleBorderWindow;
        }
    }
}
