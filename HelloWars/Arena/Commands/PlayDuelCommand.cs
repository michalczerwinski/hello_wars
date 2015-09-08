using Arena.ViewModels;
using Common.Models;

namespace Arena.Commands
{
    public class PlayDuelCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public PlayDuelCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            var nextCompetitors = _viewModel.Elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                _viewModel.Game.SetupNewGame(nextCompetitors);

                RoundResult result;

                do
                {
                    result = _viewModel.Game.PerformNextRound();
                } while (!result.IsFinished);

                _viewModel.Elimination.SetLastDuelResult(result.FinalResult);
                _viewModel.ScoreList.SaveScore(result.FinalResult);
            }
        }
    }
}