using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        private TournamentLadderViewModel _tournamentLadderViewModel;
        public List<Competitor> Competitors { get; set; }

        public UserControl GetVisualization()
        {
            if (Competitors != null)
            {
                _tournamentLadderViewModel = new TournamentLadderViewModel(Competitors);
                return new TournamentLadderControl(_tournamentLadderViewModel); ;
            }

            return null;
        }

        public IList<Competitor> GetNextCompetitors()
        {
            var result = new List<Competitor>();

            foreach (var roundList in _tournamentLadderViewModel.RoundList)
            {
                foreach (var competitor in roundList)
                {
                    if (roundList.Count > 1)
                    {
                        var competitorViewModel = competitor.ViewModel;
                        if (competitorViewModel.Competitor.StilInGame)
                        {
                            var connectedCompetitor = roundList.First(f => f.PairWithId == competitor.Id).ViewModel.Competitor;

                            if (connectedCompetitor.StilInGame)
                            {
                                result.Add(competitor.ViewModel.Competitor);
                                result.Add(connectedCompetitor);
                                return result;
                            }
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
                var competitor = new Competitor
                {
                    AvatarUrl = @"/Assets/BotImg.png",
                    Name = "Bot",
                    StilInGame = true,
                };
                Competitors.Add(competitor);
            }
        }
    }
}
