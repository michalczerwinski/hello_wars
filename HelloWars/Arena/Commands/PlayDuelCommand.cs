using System.Linq;
using Arena.Interfaces;

namespace Arena.Commands
{
    public class PlayDuelCommand : CommandBase
    {
        private readonly IElimination _elimination;
        private readonly IGame _game;

        public PlayDuelCommand(IElimination elimination, IGame game)
        {
            _elimination = elimination;
            _game = game;
        }

        public override void Execute(object parameter = null)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                _game.Competitors = nextCompetitors.ToList();

                while (_game.PerformNextRound())
                {
                }
                _elimination.SetLastDuelResult(_game.GetResoult());
            }
        }
    }
}
