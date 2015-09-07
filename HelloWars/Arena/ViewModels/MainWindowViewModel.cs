using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
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
        [ImportMany((typeof(IGame)))]
        private IEnumerable<IGame> _gamePlugins;
        [ImportMany((typeof(IElimination)))]
        private IEnumerable<IElimination> _eliminationPlugins;
        private string _headerText;
        private ArenaConfiguration _arenaConfiguration;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private string _gameLog;
        private List<ICompetitor> _competitors;
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

        public UserControl GameTypeControl
        {
            get { return _gameTypeControl; }
            set { SetProperty(ref _gameTypeControl, value); }
        }

        public string GameLog
        {
            get { return _gameLog; }
            set { SetProperty(ref _gameLog, value); }
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

        private Predicate<object> OnLoaded()
        {
            Game = _gamePlugins.FirstOrDefault(f => (f.GetType().Name == _arenaConfiguration.GameType));
            Elimination = _eliminationPlugins.FirstOrDefault(f => (f.GetType().Name == _arenaConfiguration.EliminationType));

            if (Elimination != null)
            {
                Elimination.Bots = Competitors;
                EliminationTypeControl = Elimination.GetVisualization();
            }
            if (Game != null)
            {
                GameTypeControl = Game.GetVisualisation();
            }

            return DefaultCanExecute;
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }

        public void Init(ArenaConfiguration arenaConfiguration)
        {
            ScoreList = new ScoreList();
            HeaderText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            InitMef();
            AskForCompetitors(arenaConfiguration.GameType);
        }

        private void InitMef()
        {
            var catalog = new AggregateCatalog();
            var assembly = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(assembly);
            var dictionary = new DirectoryCatalog(path);
            catalog.Catalogs.Add(dictionary);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
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
