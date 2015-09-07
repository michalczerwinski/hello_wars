using Arena.Interfaces;
using Arena.ViewModels;
using Common.Interfaces;
using Common.Utilities;

namespace Arena.Commands
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
