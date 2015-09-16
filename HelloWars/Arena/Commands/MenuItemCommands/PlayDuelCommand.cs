using System.Collections.Generic;
using Arena.ViewModels;
using Common.Models;

namespace Arena.Commands.MenuItemCommands
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
                var gameHistoryEntry = new GameHistoryEntryViewModel()
                {
                    GameDescription = _viewModel.Elimination.GetGameDescription(),
                    History = new List<RoundPartialHistory>()
                };

                _viewModel.Game.SetupNewGame(nextCompetitors);

                _viewModel.OutputText += "Game starting: " + gameHistoryEntry.GameDescription + "\n";

                RoundResult result;

                do
                {
                    result = _viewModel.Game.PerformNextRound();
                    gameHistoryEntry.History.AddRange(result.History);
                }
                while (!result.IsFinished);

                _viewModel.Elimination.SetLastDuelResult(result.FinalResult);
                _viewModel.ScoreList.SaveScore(result.FinalResult);
                _viewModel.GameHistory.Add(gameHistoryEntry);
            }
        }
    }
}