using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class ShowArenaInstruction : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public ShowArenaInstruction(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"..\src\Arena\Resources\ArenaInstruction.html");
        }
    }
}
