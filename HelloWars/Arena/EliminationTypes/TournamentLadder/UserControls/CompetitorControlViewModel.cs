using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    public class CompetitorControlViewModel : BindableBase
    {
        private Competitor _competitor;
        private int _currentDuelId;
        public int CurrentRound;
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

        public CompetitorControlViewModel(Competitor competitor)
        {
            Competitor = competitor;
        }

        public int Id;
        public int ConnectedTo;
        public int ItemConnected1;
        public int ItemConnected2;
        public int PairWithId;

        public CompetitorControlViewModel()
        {
        }
    }
}
