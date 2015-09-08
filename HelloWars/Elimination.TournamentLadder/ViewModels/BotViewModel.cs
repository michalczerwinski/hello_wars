using Common.Interfaces;
using Common.Models;

namespace Elimination.TournamentLadder.ViewModels
{
    public class BotViewModel : BindableBase
    {
        private ICompetitor _botClient;
        private bool _stilInGame;
        public int CurrentStage;

        public ICompetitor BotClient
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
