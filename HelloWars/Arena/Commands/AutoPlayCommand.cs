using Common.Interfaces;
using Common.Utilities;

namespace Arena.Commands
{
    public class AutoPlayCommand : PlayDuelCommand
    {

        public AutoPlayCommand(IElimination elimination, IGame game, ScoreList scoreList)
            : base(elimination, game, scoreList)
        {
        }

        public override void Execute(object parameter = null)
        {
            while (_elimination.GetNextCompetitors() != null)
            {
                base.Execute();
            }
        }
    }
}
