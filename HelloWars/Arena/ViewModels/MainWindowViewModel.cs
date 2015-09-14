using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Arena.Commands;
using Arena.Commands.MenuItemCommands;
using Arena.Configuration;
using Common;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Serialization;
using Common.Utilities;

namespace Arena.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<ICompetitor> _competitors;
        private ObservableCollection<GameHistoryEntryViewModel> _gameHistory;
        private ICommand _autoPlayCommand;
        private ICommand _onLoadedCommand;
        private ICommand _openCommand;
        private ICommand _closeCommand;
        private ICommand _verifyPlayersCommand;
        private ICommand _gameRulesCommand;
        private ICommand _aboutCommand;

        public ArenaConfiguration ArenaConfiguration { get; set; }
        public IElimination Elimination { get; set; }
        public IGame Game { get; set; }
        public ScoreList ScoreList { get; set; }

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

        public ICommand OnLoadedCommand
        {
            get { return _onLoadedCommand ?? (_onLoadedCommand = new LoadGameAndEliminationUserControlsOnLoadedControl(this)); }
        }

        #region MenuItems

        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new OpenCommand(this)); }
        }

        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new CloseCommand()); }
        }

        public ICommand PlayDuelCommand
        {
            get { return new PlayDuelCommand(this); }
        }

        public ICommand AutoPlayCommand
        {
            get { return _autoPlayCommand ?? (_autoPlayCommand = new AutoPlayCommand(this)); }
        }

        public ICommand VerifyPlayersCommand
        {
            get { return _verifyPlayersCommand ?? (_verifyPlayersCommand = new VerifyPlayersCommand(this)); }
        }

        public ICommand GameRulesCommand
        {
            get { return _gameRulesCommand ?? (_gameRulesCommand = new GameRulesCommand(this)); }
        }

        public ICommand AboutCommand
        {
            get { return _aboutCommand ?? (_aboutCommand = new AboutCommand(this)); }
        }

        #endregion

        public MainWindowViewModel()
        {
            ScoreList = new ScoreList();
            ArenaConfiguration = ReadConfigurationFromXML(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Resources.DefaultArenaConfigurationName);
            InitiateManagedExtensibilityFramework();
            AskForCompetitors(ArenaConfiguration.GameConfiguration.Type);
        }

        public void AskForCompetitors(string gameTypeName)
        {
            Task.Run(async () =>
            {
                var loader = new CompetitorLoadService();

                var competitorsTasks = ArenaConfiguration.BotUrls.Select(botUrl => loader.LoadCompetitorAsync(botUrl, gameTypeName)).ToList();

                Competitors = (await Task.WhenAll(competitorsTasks)).Where(competitor => competitor != null).ToList();
            }).Wait();
        }

        public ArenaConfiguration ReadConfigurationFromXML(string path)
        {
            var xmlStream = new StreamReader(path);
            var configurationFile = xmlStream.ReadToEnd();
            var serializer = new XmlSerializer<ArenaConfiguration>();

            return serializer.Deserialize(configurationFile);
        }

        public void InitiateManagedExtensibilityFramework()
        {
            var catalog = new AggregateCatalog();
            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(assembly);
            var dictionary = new DirectoryCatalog(path);
            catalog.Catalogs.Add(dictionary);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(ArenaConfiguration);
        }
    }
}
