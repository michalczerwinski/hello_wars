using Arena.ViewModels;

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
                _viewModel.SetupNewGame();

                foreach (var nextCompetitor in nextCompetitors)
                {
                    _viewModel.CurrentGame.AddCompetitor(nextCompetitor);
                }

                _viewModel.CurrentGame.Start();
                _viewModel.GameLog += string.Format("New game starting!\n{0}\n", _viewModel.Elimination.GetGameDescription());
                
                while (!_viewModel.CurrentGame.IsGameFinished())
                {
                    _viewModel.GameLog += _viewModel.CurrentGame.PerformNextRound();
                }

                _viewModel.GameLog += "\n";
                var duelResult = _viewModel.CurrentGame.GetResults();
                _viewModel.Elimination.SetLastDuelResult(duelResult);
                _viewModel.ScoreList.SaveScore(duelResult);
            }
        }
    }
}