using Bot = BotClient.BotClient;

namespace Arena.Eliminations.TournamentLadder.ViewModels
{
    public class BotViewModel : BindableBase
    {
        private Bot _botClient;
        private bool _stilInGame;
        public int CurrentStage;

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

        public BotViewModel()
        {
            StilInGame = true;
        }
    }
}
