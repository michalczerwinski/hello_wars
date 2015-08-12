using System.Windows;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.Models
{
    public  class WrappedCompetitor : BindableBase
    {
        private Competitor _competitor;

        public Competitor Competitor
        {
            get { return _competitor; }
            set { SetProperty(ref _competitor, value); }
        }

        public Point CompetitorHeadPoint { get; set; }
        public int OrderNumber { get; set; }

        public WrappedCompetitor(Competitor competitor)
        {
            Competitor = competitor;
        }
    }
}
