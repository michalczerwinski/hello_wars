using System.Windows;
using Common;

namespace Arena.Commands.MenuItemCommands
{
    class CloseCommand : CommandBase
    {
        public override void Execute(object parameter = null)
        {
            Application.Current.Shutdown();
        }
    }
}
