using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private List<Bot> _bots;
        private ICommand _playDuelCommand;
        private ICommand _autoPlayCommand;
        private Dictionary<Bot, Stack<Tuple<Bot, double>>> _scoreList;

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

        public List<Bot> Bots
        {
            get { return _bots; }
            set { SetProperty(ref _bots, value); }
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

        private void AutoPlay(object obj)
        {
            var nextBots = _elimination.GetNextBots();

            while (nextBots != null)
            {
                _game.Bots = nextBots.ToList();
                while (_game.PerformNextMove())
                {
                    Task.Delay(1000);
                }

                var duelResoult = _game.GetResoult();

                _elimination.SetLastDuelResult(duelResoult);
                SaveDuelResult(duelResoult);

                nextBots = _elimination.GetNextBots();
            }
        }

        private void SaveDuelResult(Dictionary<Bot, double> duelResoult)
        {
            if (_scoreList == null)
            {
                _scoreList = new Dictionary<Bot, Stack<Tuple<Bot, double>>>();
            }

            var duelResoultList = new List<Tuple<Bot, double>>();

            //Urgent need for refactor
            foreach (var item in duelResoult)
            {
                duelResoultList.Add(new Tuple<Bot, double>(item.Key, item.Value));
            }

            foreach (var competitor in duelResoultList)
            {
                var scoreRecord = _scoreList.FirstOrDefault(f => f.Key == competitor.Item1);

                if (scoreRecord.Key == null)
                {
                    var tempList = duelResoultList.Where(f => f.Item1 != competitor.Item1);
                    var newStack = new Stack<Tuple<Bot, double>>(tempList);

                    _scoreList.Add(competitor.Item1, newStack);
                }
                else 
                {
                    var tempList = duelResoultList.Where(f => f.Item1 != competitor.Item1);

                    foreach (var temp in tempList)
                    {
                        scoreRecord.Value.Push(temp);
                    }
                }
            }
        }

        private void PlayDuel(object obj)
        {
            var nextBots = _elimination.GetNextBots();
            if (nextBots != null)
            {
                _game.Bots = nextBots.ToList();

                while (_game.PerformNextMove())
                {

                }
                _elimination.SetLastDuelResult(_game.GetResoult());
            }
        }
    }
}
