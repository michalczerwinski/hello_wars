using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Configuration;
using Arena.Interfaces;
using BotClient;
using Bot = BotClient.BotClient;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _tempText;
        private readonly ArenaConfiguration _arenaConfiguration;
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private BotProxy _botProxy;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<Bot> _competitors;
        private ICommand _playDuelCommand;
        private ICommand _autoPlayCommand;
        private Dictionary<Bot, double> _scoreList;

        public string TempText
        {
            get { return _tempText; }
            set { SetProperty(ref _tempText, value); }
        }

        public UserControl GameTypeControl
        {
            get { return _gameTypeControl; }
            set { SetProperty(ref _gameTypeControl, value); }
        }

        public UserControl EliminationTypeControl
        {
            get { return _eliminationTypeControl; }
            set { SetProperty(ref _eliminationTypeControl, value); }
        }

        public List<Bot> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public ICommand PlayDuelCommand
        {
            get { return _playDuelCommand ?? (_playDuelCommand = new RelayCommand(PlayDuel)); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new RelayCommand(AutoPlay)); }
        }

        public MainWindowViewModel(ArenaConfiguration arenaConfiguration)
        {
            TempText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Eliminations;
            _game = arenaConfiguration.GameDescription;

            AskForCompetitors();

            _elimination.Competitors = Competitors;
            _eliminationTypeControl = _elimination.GetVisualization();
            _gameTypeControl = _game.GetVisualisation();
        }

        private void AskForCompetitors()
        {
            Competitors = new List<Bot>();

            foreach (var competitorUrl in _arenaConfiguration.CompetitorUrls)
            {
                _botProxy = new BotProxy(competitorUrl);

                var competitor = new Bot
                {
                    Url = competitorUrl,
                    AvatarUrl = _botProxy.GetAvatarUrl(),
                    Name = _botProxy.GetName(),
                };
                Competitors.Add(competitor);
            }
        }

        private void AutoPlay(object obj)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();

            while (nextCompetitors != null)
            {
                _game.Competitors = nextCompetitors.ToList();
                _game.PerformNextMove();
                _elimination.SetLastDuelResult(_game.GetResoult());
                nextCompetitors = _elimination.GetNextCompetitors();
            }

            //if (_scoreList == null) _scoreList = new Dictionary<Competitor, double>();

        }

        private void PlayDuel(object obj)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                _game.Competitors = nextCompetitors.ToList();
                _game.PerformNextMove();
            }
            _elimination.SetLastDuelResult(_game.GetResoult());
        }
    }
}
