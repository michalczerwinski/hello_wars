using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Configuration;
using Arena.Interfaces;
using Arena.Models;
using BotClient;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _tempText;
        private readonly ArenaConfiguration _arenaConfiguration;
        private readonly IElimination _elimination;
        private readonly IGameProvider _gameProvider;
        private BotProxy _botProxy;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<Competitor> _competitors;
        private ICommand _playDuelCommand;
        private ICommand _autoPlayCommand;
        private ICommand _startDuelCommand;
        private ICommand _stopDuelCommand;

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

        public List<Competitor> Competitors
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

        public ICommand StartDuelCommand
        {
            get { return _startDuelCommand ?? (_startDuelCommand = new RelayCommand(StartDuel)); }
        }

        public ICommand StopDuelCommand
        {
            get { return _stopDuelCommand ?? (_stopDuelCommand = new RelayCommand(StopDuel)); }
        }

        public MainWindowViewModel(ArenaConfiguration arenaConfiguration)
        {
            TempText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Eliminations;
            _gameProvider = arenaConfiguration.GameDescription;

            AskForCompetitors();

            _elimination.Competitors = Competitors;
            _eliminationTypeControl = _elimination.GetVisualization();
            _gameTypeControl = _gameProvider.GetVisualisation();
        }

        private void AskForCompetitors()
        {
            Competitors = new List<Competitor>();

            foreach (var competitorUrl in _arenaConfiguration.CompetitorUrls)
            {
                _botProxy = new BotProxy(competitorUrl);

                var competitor = new Competitor
                {
                    Url = competitorUrl,
                    AvatarUrl = _botProxy.GetAvatarUrl(),
                    Name = _botProxy.GetName(),
                    StilInGame = true,
                };
                Competitors.Add(competitor);
            }
        }

        private void AutoPlay(object obj)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();

            while (nextCompetitors != null)
            {
                var game = _gameProvider.CreateNewGame(nextCompetitors);
                game.PerformNextMove();
                nextCompetitors = _elimination.GetNextCompetitors();
            }
        }

        private void PlayDuel(object obj)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();
            if (nextCompetitors != null)
            {
                var game = _gameProvider.CreateNewGame(nextCompetitors);
                game.PerformNextMove();
            }
        }

        private void StopDuel(object obj)
        {
            //_duelInRun = false;
        }

      //  private bool _duelInRun;

        private void StartDuel(object obj)
        {
            //_duelInRun = true;
            //while (_duelInRun)
            //{
            //    Task.Delay(5000);
            //}
        }
    }
}
