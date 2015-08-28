using System;
using System.Collections.Generic;
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
        private string _headerText;
        private readonly ArenaConfiguration _arenaConfiguration;
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private BotProxy _botProxy;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<Bot> _bots;
        private ICommand _playDuelCommand;
        private ICommand _autoPlayCommand;
        private Dictionary<Bot, Stack<Tuple<Bot, double>>> _scoreList;

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

        public List<Bot> Bots
        {
            get { return _bots; }
            set { SetProperty(ref _bots, value); }
        }

        public ICommand PlayDuelCommand
        {
            get { return new PlayDuelCommand(_elimination, _game); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(_elimination, _game, _scoreList)); }
        }

        public MainWindowViewModel(ArenaConfiguration arenaConfiguration)
        {
            HeaderText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Elimination;
            _game = arenaConfiguration.Game;

            AskForBots();

            _elimination.Bots = Bots;
            _eliminationTypeControl = _elimination.GetVisualization();
            _gameTypeControl = _game.GetVisualisation();
        }

        private void AskForBots()
        {
            Bots = new List<Bot>();

            foreach (var botUrl in _arenaConfiguration.BotUrls)
            {
                _botProxy = new BotProxy(botUrl);

                var bot = new Bot
                {
                    Url = botUrl,
                    AvatarUrl = _botProxy.GetAvatarUrl(),
                    Name = _botProxy.GetName(),
                };
                Bots.Add(bot);
            }
        }
    }
}
