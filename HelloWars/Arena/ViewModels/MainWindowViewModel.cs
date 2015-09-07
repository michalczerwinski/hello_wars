using System.Collections.Generic;
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
        private ArenaConfiguration _arenaConfiguration;
        private IElimination _elimination;
        private IGame _game;
        private UserControl _gameTypeControl;
        private UserControl _eliminationTypeControl;
        private List<ICompetitor> _competitors;
        private ICommand _autoPlayCommand;
        private ScoreList _scoreList;

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

        public void Init(ArenaConfiguration arenaConfiguration)
        {
            _scoreList = new ScoreList();
            HeaderText = "Hello Wars();";
            _arenaConfiguration = arenaConfiguration;
            _elimination = arenaConfiguration.Elimination;
            var gameType = TypeHelper<IGame>.GetGameType(arenaConfiguration.GameType);
            _game = TypeHelper<IGame>.CreateInstance(gameType);

            AskForCompetitors(arenaConfiguration.GameType);

            _elimination.Bots = Competitors;
            _eliminationTypeControl = _elimination.GetVisualization();
            _gameTypeControl = _game.GetVisualisation();
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
