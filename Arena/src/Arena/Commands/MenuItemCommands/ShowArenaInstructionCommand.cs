using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class ShowArenaInstruction : CommandBase
    {
        public override void Execute(object parameter = null)
        {
            System.Diagnostics.Process.Start(@"Resources\ArenaInstruction.html");
        }
    }
}
