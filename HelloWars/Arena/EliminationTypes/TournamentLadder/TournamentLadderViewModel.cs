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
        private List<Tuple<CompetitorControlViewModel, CompetitorControlViewModel>> _duelPairList;

        public List<Competitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public List<Tuple<CompetitorControlViewModel, CompetitorControlViewModel>> DuelPairList
        {
            get { return _duelPairList; }
            set { SetProperty(ref _duelPairList, value); }
        }

        public int CurrentRound
        {
            get { return _currentRound; }
            set { SetProperty(ref _currentRound, value); }
        }

        public List<List<CompetitorViewControl>> RoundList { get; set; }

        public TournamentLadderViewModel(List<Competitor> competitors)
        {
            Competitors = competitors;
            competitors.First().DuelFinished += OnDuelFinished;
            //    Competitors = WrapCompetitors(competitors);
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
                item.DataContext = itemConnected2;
            }
            else if (itemConnected22 != null && itemConnected22.Competitor.StilInGame)
            {
                item.DataContext = itemConnected1;
            }
            else
            {
                throw new Exception("something goes wrong.");
            }
        }

        private void CreateRoundList(int roundNumber, int competitorsNumber)
        {
            var result = new List<Competitor>();

            for (int i = 0; i < competitorsNumber; i++)
            {
                result.Add(new Competitor());
            }


        }

        private List<CompetitorControlViewModel> WrapCompetitors(List<Competitor> competitors)
        {
            var result = new List<CompetitorControlViewModel>();

            foreach (var competitor in competitors)
            {
                var wrappedCompetitor = new CompetitorControlViewModel(competitor);

                result.Add(wrappedCompetitor);
            }

            return result;
        }
    }
}
