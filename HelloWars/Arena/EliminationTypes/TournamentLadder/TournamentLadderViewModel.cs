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
        public List<List<CompetitorViewControl>> StageLists;

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

            var competitorControl = StageLists[competitorViewModel.CurrentStage].First(f => f.ViewModel == competitorViewModel);

            var nextStageItem = StageLists[competitorViewModel.CurrentStage + 1].First(f => f.ItemConnected1 == competitorControl.Id || f.ItemConnected2 == competitorControl.Id);
            var itemConnected1 = StageLists[competitorViewModel.CurrentStage].FirstOrDefault(f => f.Id == nextStageItem.ItemConnected1).ViewModel;
            var itemConnected2 = StageLists[competitorViewModel.CurrentStage].FirstOrDefault(f => f.Id == nextStageItem.ItemConnected2).ViewModel;

            if (itemConnected1 != null && itemConnected1.Competitor.StilInGame)
            {
                itemConnected1.CurrentStage++;
                nextStageItem.ViewModel = itemConnected1;
            }
            else if (itemConnected2 != null && itemConnected2.Competitor.StilInGame)
            {
                itemConnected2.CurrentStage++;
                nextStageItem.ViewModel = itemConnected2;
            }
            else
            {
                throw new Exception("something goes wrong.");
            }
        }

        private List<CompetitorControlViewModel> GetCompetitorViewModelList()
        {
            var result = new List<CompetitorControlViewModel>();

            foreach (var stageList in StageLists)
            {
                result.AddRange(stageList.Select(f => (CompetitorControlViewModel)f.DataContext));
            }
            return result;
        }
    }
}
