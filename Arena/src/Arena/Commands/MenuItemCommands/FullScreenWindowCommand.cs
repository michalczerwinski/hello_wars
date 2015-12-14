using System.Windows;
using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class FullScreenWindowCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public FullScreenWindowCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            if (_viewModel.IsFullScreenApplied)
            {
                _viewModel.WindowState = WindowState.Maximized;
                _viewModel.WindowStyle = WindowStyle.None;
            }
            else
            {
                _viewModel.WindowState = WindowState.Normal;
                _viewModel.WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
