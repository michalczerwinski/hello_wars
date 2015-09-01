using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Configuration;
using Arena.Interfaces;
using Arena.Models;
using BotClient;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _tempText;
        private ArenaConfiguration _arenaConfiguration;
        private IElimination _elimination;
        private IGameDescription _gameDescription;
        private BotProxy _botProxy;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<Competitor> _competitors;

        public string TempText
        {
            get { return _tempText; }
            set { SetProperty(ref _tempText, value); }
        }

        public UserControl GameTypeControl
        {
            get { return null; }
            set { SetProperty(ref _gameTypeControl, value); }
        }

        public UserControl EliminationTypeControl
        {
            get { return _elimination.GetVisualization(Competitors); }
            set { SetProperty(ref _eliminationTypeControl, value); }
        }

        public List<Competitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public MainWindowViewModel(ArenaConfiguration arenaConfiguration)
        {
            TempText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Eliminations;
            _gameDescription = arenaConfiguration.GameDescription;

            AskForCompetitors();
        }

        private void AskForCompetitors()
        {
            Competitors = new List<Competitor>();

            foreach (var competitorUrl in _elimination.Competitors)
            {
                _botProxy = new BotProxy(competitorUrl);

                var competitor = new Competitor
                {
                    AvatarUrl = _botProxy.GetAvatarUrl(),
                    Name = _botProxy.GetName(),
                    StilInGame = true,
                };

                Competitors.Add(competitor);
            }
        }
    }
}
