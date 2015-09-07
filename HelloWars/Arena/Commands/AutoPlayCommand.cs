using Arena.Interfaces;
using Arena.ViewModels;
using Common.Interfaces;
using Common.Utilities;

namespace Arena.Commands
{
    public class AutoPlayCommand : PlayDuelCommand
    {

        public AutoPlayCommand(MainWindowViewModel caller) : base(caller)
        {
        }

        public override void Execute(object parameter = null)
        {
            while (_caller.Elimination.GetNextCompetitors() != null)
            {
                base.Execute();
            }
        }
    }
}
