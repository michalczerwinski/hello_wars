using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.EliminationTypes.TournamentLadder.Models;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel : BindableBase
    {
        private List<WrappedCompetitor> _competitors;
        private int _roundNumber;
        private List<Tuple<WrappedCompetitor, WrappedCompetitor>> _duelPairList;

        public List<WrappedCompetitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public List<Tuple<WrappedCompetitor, WrappedCompetitor>> DuelPairList
        {
            get { return _duelPairList; }
            set { SetProperty(ref _duelPairList, value); }
        }

        public int RoundNumber
        {
            get { return _roundNumber; }
            set { SetProperty(ref _roundNumber, value); }
        }

        public TournamentLadderViewModel(List<Competitor> competitors)
        {
            Competitors =new List<WrappedCompetitor>();

            foreach (var competitor in competitors)
            {
                Competitors.Add(new WrappedCompetitor(competitor));
            }
        }
    }
}
