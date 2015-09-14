using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Threading;
using Arena.Commands;
using Arena.Configuration;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _headerText;
        private ArenaConfiguration _arenaConfiguration { get; set; }
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<ICompetitor> _competitors;
        private ObservableCollection<GameHistoryEntryViewModel> _gameHistory; 
        private ICommand _autoPlayCommand;
        private ICommand _onLoadedCommand;
        private string _output;
        private static object _lock = new object();

        public IElimination Elimination { get; set; }
        public IGame Game { get; set; }
        public ScoreList ScoreList { get; set; }

        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
        }

        public string Output
        {
            get { return _output; }
            set { SetProperty(ref _output, value); }
        }

        public ObservableCollection<GameHistoryEntryViewModel> GameHistory
        {
            get { return _gameHistory ?? (_gameHistory = new ObservableCollection<GameHistoryEntryViewModel>()); }
            set { SetProperty(ref _gameHistory, value); }
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
            get { return new PlayDuelCommand(this); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(this)); }
        }

        public ICommand OnLoadedCommand
        {
            get { return _onLoadedCommand ?? (_onLoadedCommand = new CommandBase(OnLoaded())); }
        }

        public MainWindowViewModel()
        {
            ScoreList = new ScoreList();
            HeaderText = "Hello Wars();";
        }

        private Predicate<object> OnLoaded()
        {
            Elimination = _arenaConfiguration.Elimination;
            Game = _arenaConfiguration.Game;

            Output += "Elimination loaded: " + _arenaConfiguration.EliminationConfiguration.Type + "\n";
            Output += "Game loaded: " + _arenaConfiguration.GameConfiguration.Type + "\n";

            Competitors = _arenaConfiguration.BotUrls.Select(s => new Competitor() {Id = Guid.NewGuid(), Name = "Connecting...", Url = s} as ICompetitor).ToList();

            if (Elimination != null)
            {
                Elimination.Bots = Competitors;
                EliminationTypeControl = Elimination.GetVisualization(_arenaConfiguration.EliminationConfiguration);
            }
            if (Game != null)
            {
                GameTypeControl = Game.GetVisualisationUserControl(_arenaConfiguration.GameConfiguration);
            }

            return DefaultCanExecute;
        }

        public void OnRendered()
        {
            AskForCompetitors(_arenaConfiguration.GameConfiguration.Type, Competitors); 
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }

        public void ReadConfiguration(ArenaConfiguration arenaConfiguration)
        {
            _arenaConfiguration = arenaConfiguration;
        }

        private void AskForCompetitors(string gameTypeName, List<ICompetitor> emptyCompetitors)
        {
            Output += string.Format("Waiting for players ({0})\n", emptyCompetitors.Count);
            var dispatcher = _eliminationTypeControl.Dispatcher;
            
            Task.Run(() =>
            {
                var loader = new CompetitorLoadService();

                var competitorsTasks = emptyCompetitors.Select(async bot =>
                {
                    var competitor = await loader.LoadCompetitorAsync(bot.Url, gameTypeName);

                    bot.Name = competitor.Name;
                    bot.AvatarUrl = competitor.AvatarUrl;

                    lock (_lock)
                    {
                        Output += string.Format("Bot \"{0}\" connected!\n", bot.Name);

                        dispatcher.Invoke(Elimination.UpdateControl);
                    }

                    return bot;
                }).ToList();

                Task.WhenAll(competitorsTasks).ContinueWith(task =>
                {
                    Output += "All players connected!\n";
                });

            });
        }
    }
}
