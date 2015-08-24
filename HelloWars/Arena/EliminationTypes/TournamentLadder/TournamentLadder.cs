using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        public List<Competitor> Competitors { get; set; }

        private TournamentLadderViewModel _tournamentLadderViewModel;
        private TournamentLadderControl _tournamentLadderControl;

        public UserControl GetVisualization()
        {
            if (Competitors != null)
            {
                var startingNumberOfCompetitors = CalculateCompetitorCount(Competitors);

                AddBotsToCompetitorsList(startingNumberOfCompetitors - Competitors.Count);
                _tournamentLadderViewModel = new TournamentLadderViewModel(Competitors);
                _tournamentLadderControl = new TournamentLadderControl(_tournamentLadderViewModel);

                return _tournamentLadderControl;
            }
            return null;
        }

        public IList<Competitor> GetNextCompetitors()
        {
            var result = new List<Competitor>();

            foreach (var roundList in _tournamentLadderViewModel.RoundList)
            {
                var competitorViewModelList = roundList.Select(f => (CompetitorControlViewModel)f.DataContext).ToList();

                foreach (var competitor in competitorViewModelList)
                {
                    if (competitor.Competitor.StilInGame)
                    {
                        var connectedCompetitor = competitorViewModelList.First(f => f.PairWithId == competitor.Id).Competitor;
                        if (connectedCompetitor.StilInGame)
                        {
                            result.Add(competitor.Competitor);
                            result.Add(connectedCompetitor);
                            return result;
                        }
                    }
                }
            }

            return null;
        }

        private void AddBotsToCompetitorsList(int botCount)
        {
            for (int i = 0; i < botCount; i++)
            {
                var competitor = new Competitor();
                Competitors.Add(competitor);
            }
        }

        private static int CalculateCompetitorCount(List<Competitor> competitors)
        {
            var competitorCount = competitors.Count;
            if (competitorCount < 3) competitorCount = 2;
            if (competitorCount > 2 && competitorCount < 5)
            {
                competitorCount = 4;
            }
            if (competitorCount > 4 && competitorCount < 9)
            {
                competitorCount = 8;
            }
            if (competitorCount > 8 && competitorCount < 17)
            {
                competitorCount = 16;
            }
            if (competitorCount > 16 && competitorCount < 33)
            {
                competitorCount = 32;
            }
            if (competitorCount > 32 && competitorCount < 65)
            {
                competitorCount = 32;
            }
            return competitorCount;
        }
    }
}
