using Arena.ViewModels;

namespace Arena.Commands.MenuItemCommands
{
    public class AutoPlayCommand : PlayDuelCommand
    {
        public AutoPlayCommand(MainWindowViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object parameter = null)
        {
            while (_viewModel.Elimination.GetNextCompetitors() != null)
            {
                base.Execute(parameter);
            }
        }
    }
}
