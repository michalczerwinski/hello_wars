using System.Windows;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.Models
{
    public  class WrappedCompetitor
    {
        public Competitor Competitor { get; set; }
        public Point CompetitorHeadPoint { get; set; }
        public int OrderNumber { get; set; }

        public WrappedCompetitor(Competitor competitor)
        {
            Competitor = competitor;
        }
    }
}
