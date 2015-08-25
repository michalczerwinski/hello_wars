using System;
using System.Collections.Generic;
using System.Linq;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel
    {
        private List<CompetitorControlViewModel> _competitorViewModels;
        public List<Competitor> Competitors;
        public List<List<CompetitorViewControl>> RoundList;

        public TournamentLadderViewModel(List<Competitor> competitors)
        {
            Competitors = competitors;

            foreach (var competitor in competitors)
            {
                competitor.DuelFinished += OnDuelFinished;
            }
        }

        private void OnDuelFinished(object sender)
        {
            if (_competitorViewModels == null) { _competitorViewModels = GetCompetitorViewModelList(); }
            var competitor = sender as Competitor;
            var competitorViewModel = _competitorViewModels.FirstOrDefault(f => f.Competitor == competitor);

            var competitorControl = RoundList[competitorViewModel.CurrentRound].First(f => f.ViewModel == competitorViewModel);

            var nextRoundItem = RoundList[competitorViewModel.CurrentRound + 1].First(f => f.ItemConnected1 == competitorControl.Id || f.ItemConnected2 == competitorControl.Id);
            var itemConnected1 = RoundList[competitorViewModel.CurrentRound].FirstOrDefault(f => f.Id == nextRoundItem.ItemConnected1).ViewModel;
            var itemConnected2 = RoundList[competitorViewModel.CurrentRound].FirstOrDefault(f => f.Id == nextRoundItem.ItemConnected2).ViewModel;

            if (itemConnected1 != null && itemConnected1.Competitor.StilInGame)
            {
                itemConnected1.CurrentRound++;
                nextRoundItem.ViewModel = itemConnected1;
            }
            else if (itemConnected2 != null && itemConnected2.Competitor.StilInGame)
            {
                itemConnected2.CurrentRound++;
                nextRoundItem.ViewModel = itemConnected2;
            }
            else
            {
                throw new Exception("something goes wrong.");
            }
        }

        private List<CompetitorControlViewModel> GetCompetitorViewModelList()
        {
            var result = new List<CompetitorControlViewModel>();

            foreach (var roundList in RoundList)
            {
                result.AddRange(roundList.Select(f => (CompetitorControlViewModel)f.DataContext));
            }
            return result;
        }
    }
}
