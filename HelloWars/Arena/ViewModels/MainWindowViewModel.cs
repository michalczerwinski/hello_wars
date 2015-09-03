using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Configuration;
using Arena.Helpers;
using Arena.Interfaces;
using Arena.Utilities;
using Game.Common.Helpers;
using Game.Common.Interfaces;
using Game.Common.Models;
using Game.TicTacToe.Models;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _headerText;
        private readonly ArenaConfiguration _arenaConfiguration;
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<ICompetitor> _competitors;
        private ICommand _autoPlayCommand;
        private readonly ScoreList _scoreList;

        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
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

        public List<ICompetitor> Competitors
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public ICommand PlayDuelCommand
        {
            get { return new PlayDuelCommand(_elimination, _game, _scoreList); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(_elimination, _game, _scoreList)); }
        }

        public MainWindowViewModel(ArenaConfiguration arenaConfiguration)
        {
            _scoreList = new ScoreList();
            HeaderText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Elimination;
            var gameType = TypeHelper<IGame>.GetGameType(arenaConfiguration.GameType);
            _game = TypeHelper<IGame>.CreateInstance(gameType);

            AskForCompetitors();

            _elimination.Bots = Competitors;
            _eliminationTypeControl = _elimination.GetVisualization();
            _gameTypeControl = _game.GetVisualisation();
        }

        private void AskForCompetitors()
        {
            Competitors = new List<ICompetitor>();

            foreach (var botUrl in _arenaConfiguration.BotUrls)
            {
                //TODO: use botFactory and download information via provided URLs
                var bot = new Competitor()
                {
                    Id = Guid.NewGuid(),
                    AvatarUrl = null,
                    Name = botUrl,
                    Url = null
                };
                Competitors.Add(bot);
            }
        }
    }
}
