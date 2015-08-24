using System;
using System.Collections.Generic;
using System.Linq;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel : BindableBase
    {
        private List<Competitor> _competitors;
        private int _currentRound;

        public List<Competitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public int CurrentRound
        {
            get { return _currentRound; }
            set { SetProperty(ref _currentRound, value); }
        }

        public List<List<CompetitorViewControl>> RoundList { get; set; }

        public TournamentLadderViewModel(List<Competitor> competitors)
        {
            CurrentRound = 0;
            Competitors = competitors;
            foreach (var competitor in competitors)
            {
                competitor.DuelFinished += OnDuelFinished;
            }
        }

        private void OnDuelFinished(object sender)
        {
            var competitor = sender as Competitor;
            var competitorViewModel = (CompetitorControlViewModel)RoundList[CurrentRound].FirstOrDefault(f => ((CompetitorControlViewModel)f.DataContext).Competitor == competitor).DataContext;

            var item = RoundList[competitorViewModel.CurrentRound + 1].First(f => ((CompetitorControlViewModel)f.DataContext).ItemConnected1 == competitorViewModel.Id || ((CompetitorControlViewModel)f.DataContext).ItemConnected2 == competitorViewModel.Id);
            var itemConnected1 = RoundList[competitorViewModel.CurrentRound].FirstOrDefault(f => ((CompetitorControlViewModel)f.DataContext).Id == ((CompetitorControlViewModel)item.DataContext).ItemConnected1).DataContext;
            var itemConnected11 = itemConnected1 as CompetitorControlViewModel;

            var itemConnected2 = RoundList[competitorViewModel.CurrentRound].FirstOrDefault(f => ((CompetitorControlViewModel)f.DataContext).Id == ((CompetitorControlViewModel)item.DataContext).ItemConnected2).DataContext;
            var itemConnected22 = itemConnected2 as CompetitorControlViewModel;


            if (itemConnected11 != null && itemConnected11.Competitor.StilInGame)
            {
                itemConnected22.CurrentRound++;
                item.DataContext = itemConnected22;

            }
            else if (itemConnected22 != null && itemConnected22.Competitor.StilInGame)
            {
                itemConnected11.CurrentRound++;
                item.DataContext = itemConnected1;
            }
            else
            {
                throw new Exception("something goes wrong.");
            }
        }
    }
}
