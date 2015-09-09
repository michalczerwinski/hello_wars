using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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

        public IElimination Elimination { get; set; }
        public IGame Game { get; set; }
        public ScoreList ScoreList { get; set; }

        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
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

            if (Elimination != null)
            {
                Elimination.Bots = Competitors;
                EliminationTypeControl = Elimination.GetVisualization();
            }
            if (Game != null)
            {
                GameTypeControl = Game.GetVisualisationUserControl();
            }

            return DefaultCanExecute;
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }

        public void ReadConfiguration(ArenaConfiguration arenaConfiguration)
        {
            _arenaConfiguration = arenaConfiguration;
            AskForCompetitors(arenaConfiguration.GameType);
        }

        private void AskForCompetitors(string gameTypeName)
        {
            Task.Run(async () =>
            {
                var loader = new CompetitorLoadService();

                var competitorsTasks = _arenaConfiguration.BotUrls.Select(botUrl => loader.LoadCompetitorAsync(botUrl, gameTypeName)).ToList();

                Competitors = (await Task.WhenAll(competitorsTasks)).Where(competitor => competitor != null).ToList();
            }).Wait();
        }
    }
}
