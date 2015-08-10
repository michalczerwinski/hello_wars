using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel : BindableBase
    {
        private List<Competitor> _competitors;
        private int _roundNumber;

        public List<Competitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public int RoundNumber
        {
            get { return _roundNumber; }
            set { SetProperty(ref _roundNumber, value); }
        }

        public TournamentLadderViewModel(List<Competitor> competitors)
        {
            Competitors = competitors;
        }
    }
}
