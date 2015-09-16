using System.IO;
using Arena.ViewModels;

namespace Arena.Commands.MenuItemCommands
{
    class GameRulesCommand: CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public GameRulesCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            using (StreamWriter sw = new StreamWriter("GameRules.html"))
            {
                sw.Write(_viewModel.Game.GetGameRules());
            }

            System.Diagnostics.Process.Start("GameRules.html");
        }
    }
}
