using System.Windows;
using Arena.Views;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class AboutCommand : CommandBase
    {
        public override void Execute(object parameter = null)
        {
            var window = new AboutWindow
            {
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }
    }
}
