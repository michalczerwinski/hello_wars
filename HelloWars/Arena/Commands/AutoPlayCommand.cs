using System.Linq;
using Arena.Interfaces;
using Arena.Utilities;
using Bot = BotClient.BotClient;

namespace Arena.Commands
{
    public class AutoPlayCommand : CommandBase
    {
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private readonly ScoreList _scoreList;

        public AutoPlayCommand(IElimination elimination, IGame game, ScoreList scoreList)
        {
            _scoreList = scoreList;
            _elimination = elimination;
            _game = game;
        }

        public override void Execute(object parameter = null)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();

            while (nextCompetitors != null)
            {
                _game.Competitors = nextCompetitors.ToList();

                while (_game.PerformNextRound())
                {
                }

                var duelResoult = _game.GetResoult();
                _elimination.SetLastDuelResult(duelResoult);
                _scoreList.SaveScore(duelResoult);
                nextCompetitors = _elimination.GetNextCompetitors();
            }
        }
    }
}
