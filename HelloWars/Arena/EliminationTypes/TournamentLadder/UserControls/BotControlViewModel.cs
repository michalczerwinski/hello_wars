using Bot = BotClient.BotClient;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    public class BotControlViewModel : BindableBase
    {
        private Bot _botClient;
        public int CurrentStage;
        private bool _stilInGame;

        public Bot BotClient
        {
            get { return _botClient; }
            set { SetProperty(ref _botClient, value); }
        }

        public bool StilInGame
        {
            get { return _stilInGame; }
            set { SetProperty(ref _stilInGame, value); }
        }

        public BotControlViewModel()
        {
            StilInGame = true;
        }
    }
}
