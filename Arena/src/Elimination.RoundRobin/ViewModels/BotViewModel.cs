using System;
using System.Collections.Generic;
using Common.Interfaces;
using Common.Utilities;

namespace Elimination.RoundRobin.ViewModels
{
    public class BotViewModel : BindableBase
    {
        private ICompetitor _competitor;
        public HashSet<Guid> PlayAgainstId;
        private int _wins;
        private int _loses;

        public ICompetitor Competitor
        {
            get { return _competitor; }
            set { SetProperty(ref _competitor, value); }
        }

        public int Wins
        {
            get { return _wins; }
            set { SetProperty(ref _wins, value); }
        }

        public int Loses
        {
            get { return _loses; }
            set { SetProperty(ref _loses, value); }
        }

        public BotViewModel(ICompetitor competitor)
        {
            Competitor = competitor;
            Wins = 0;
            Loses = 0;
            PlayAgainstId = new HashSet<Guid>();
        }
    }
}
