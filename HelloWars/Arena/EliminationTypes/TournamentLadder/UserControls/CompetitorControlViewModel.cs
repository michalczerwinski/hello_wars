using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    public class CompetitorControlViewModel : BindableBase
    {
        public int Id;
        private Competitor _competitor;
        public int ConnectedTo;
        public int ItemConnected1;
        public int ItemConnected2;
        public int PairWithId;
        public int CurrentRound;
        private int _currentDuelId;

        public int CurrentDuelId
        {
            get { return _currentDuelId; }
            set { SetProperty(ref _currentDuelId, value); }
        }

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
