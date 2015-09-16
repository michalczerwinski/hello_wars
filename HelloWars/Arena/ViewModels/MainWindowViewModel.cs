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
        private bool _isHistoryVisible;
        private bool _isOutputVisible;
        private int _selectedTabIndex;
        private string _outputText;
        private static readonly object _lock = new object();
        
        private ICommand _autoPlayCommand;
        private ICommand _onLoadedCommand;
        private ICommand _openCommand;
        private ICommand _closeCommand;
        private ICommand _verifyPlayersCommand;
        private ICommand _gameRulesCommand;
        private ICommand _aboutCommand;
        private ICommand _toggleHistoryCommand;

        public ArenaConfiguration ArenaConfiguration { get; set; }
        public IElimination Elimination { get; set; }
        public IGame Game { get; set; }
        public ScoreList ScoreList { get; set; }

        public string OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }

        public bool IsHistoryVisible
        {
            get { return _isHistoryVisible; }
            set { SetProperty(ref _isHistoryVisible, value); }
        }

        public bool IsOutputVisible
        {
            get { return _isOutputVisible; }
            set { SetProperty(ref _isOutputVisible, value); }
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { SetProperty(ref _selectedTabIndex, value); }
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

        public ICommand ToggleHistoryCommand
        {
            get { return _toggleHistoryCommand ?? (_toggleHistoryCommand = new ToggleHistoryCommand(this)); }
        }

        #endregion

        public MainWindowViewModel()
        {
            ScoreList = new ScoreList();
            IsHistoryVisible = true;
            IsOutputVisible = true;
            ApplyConfiguration(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Resources.DefaultArenaConfigurationName);
        }

        public void AskForCompetitors(string gameTypeName, List<ICompetitor> emptyCompetitors)
        {
            OutputText += string.Format("Waiting for players ({0})\n", emptyCompetitors.Count);
            
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
                        OutputText += string.Format("Bot \"{0}\" connected!\n", bot.Name);
                        Elimination.Bots.First(f => f.Id == bot.Id).Name = bot.Name;
                    }

                    return bot;
                }).ToList();

                Task.WhenAll(competitorsTasks).ContinueWith(task =>
                {
                    OutputText += "All players connected!\n";
                });
            });
        }

        public void ApplyConfiguration(string configFilePath)
        {
            ArenaConfiguration = ReadConfigurationFromXML(configFilePath);
            InitiateManagedExtensibilityFramework();
            Competitors = ArenaConfiguration.BotUrls.Select(url => new Competitor()
            {
                Id = Guid.NewGuid(),
                Name = "Connecting...",
                Url = url
            } as ICompetitor).ToList();
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
