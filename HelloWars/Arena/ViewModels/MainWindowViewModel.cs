using System;
using System.Collections.Generic;
using System.Linq;
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
        private ArenaConfiguration _arenaConfiguration;
        private IElimination _elimination;
        private IGameDescription _gameDescription;
        private BotProxy _botProxy;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<Competitor> _competitors;
        private ICommand _playDuelCommand;
        private ICommand _autoPlayCommand;
        private ICommand _playRoundCommand;

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

        public ICommand PlayRoundCommand
        {
            get { return _playRoundCommand ?? (_playRoundCommand = new RelayCommand(PlayRound)); }
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
            var _game = new Games.Tanks.Tanks();

            _gameDescription = arenaConfiguration.GameDescription;

            AskForCompetitors();

            _elimination.Competitors = Competitors;
            _eliminationTypeControl = _elimination.GetVisualization();
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
                _gameDescription.CreateNewGame(nextCompetitors);
                nextCompetitors = _elimination.GetNextCompetitors();
            }
        }

        private void PlayRound(object obj)
        {
            Competitors.First().StilInGame = false;
        }

        private void PlayDuel(object obj)
        {
            _gameDescription.CreateNewGame(_elimination.GetNextCompetitors());
        }
    }
}
