using System.Linq;
using Arena.Interfaces;
using Arena.Utilities;

namespace Arena.Commands
{
    public class PlayDuelCommand : CommandBase
    {
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private readonly ScoreList _scoreList;

        public PlayDuelCommand(IElimination elimination, IGame game, ScoreList scoreList)
        {
            _scoreList = scoreList;
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

                var duelResoult = _game.GetResoult();
                _elimination.SetLastDuelResult(duelResoult);
                _scoreList.SaveScore(duelResoult);
            }
        }
    }
}
