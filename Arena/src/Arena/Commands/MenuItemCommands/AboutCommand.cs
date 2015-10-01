using System.Windows;
using Arena.ViewModels;
using Arena.Views;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class AboutCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public AboutCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            var window = new AboutWindow()
            {
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }
    }
}
