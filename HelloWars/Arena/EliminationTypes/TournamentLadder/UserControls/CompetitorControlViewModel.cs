using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    public class CompetitorControlViewModel : BindableBase
    {
        private Competitor _competitor;
        public int CurrentRound;

        public Competitor Competitor
        {
            get { return _competitor; }
            set { SetProperty(ref _competitor, value); }
        }

        public CompetitorControlViewModel()
        {
        }

        public CompetitorControlViewModel(Competitor competitor)
        {
            Competitor = competitor;
        }
    }
}
