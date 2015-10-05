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
         //   var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"/Resources/ArenaInstruction.html");

         //   using (var sw = new StreamReader("ArenaInstruction.html"))
         //   {
         //       sw.Read();
         //       // sw.Write(new Uri("/Arena;component/Resources/ArenaInstruction.html", UriKind.Relative));
         //   }

         //var fo = File.Open(@"C:\Users\mariusz.iwanski\Documents\HELOWARS\Arena\src\Arena\Resources\ArenaInstruction.html", FileMode.Open);

         System.Diagnostics.Process.Start(@"C:\Users\mariusz.iwanski\Documents\HELOWARS\Arena\src\Arena\Resources\ArenaInstruction.html");
        }
    }
}
