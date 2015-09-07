using Arena.ViewModels;

namespace Arena.Commands
{
    public class PlayDuelCommand : CommandBase
    {
        protected readonly MainWindowViewModel _caller;

        public PlayDuelCommand(MainWindowViewModel caller)
        {
            _caller = caller;
        }

        public override void Execute(object parameter = null)
        {
            var nextCompetitors = _caller.Elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                _caller.Game.Reset();
                foreach (var nextCompetitor in nextCompetitors)
                {
                    _caller.Game.AddCompetitor(nextCompetitor);
                }

                _caller.Game.Start();
                _caller.GameLog += string.Format("New game starting!\n{0}\n", _caller.Elimination.GetGameDescription());
                
                while (!_caller.Game.IsGameFinished())
                {
                    _caller.GameLog += _caller.Game.PerformNextRound();
                }

                _caller.GameLog += "\n";
                var duelResult = _caller.Game.GetResults();
                _caller.Elimination.SetLastDuelResult(duelResult);
                _caller.ScoreList.SaveScore(duelResult);
            }
        }
    }
}