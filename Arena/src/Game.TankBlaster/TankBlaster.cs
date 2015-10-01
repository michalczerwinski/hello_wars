using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Interfaces;
using Common.Models;
using Common.Serialization;
using Game.TankBlaster.Models;
using Game.TankBlaster.Properties;
using Game.TankBlaster.Services;
using Game.TankBlaster.UserControls;

namespace Game.TankBlaster
{
    public class TankBlaster : IGame
    {
        private Battlefield _field;
        private BotService _botService;
        private int _delayTime;
        private ExplosionService _explosionService;
        private TankBlasterConfig _gameConfig;
        private LocationService _locationService;
        private int _roundNumber;

        public TankBlaster()
        {
            var xmlStream = new StreamReader("TankBlaster.config.xml");
            var configurationXml = xmlStream.ReadToEnd();
            ApplyConfiguration(configurationXml);
        }

        public async Task<RoundResult> PerformNextRoundAsync()
        {
            _roundNumber++;

            await _explosionService.HandleExplodablesAsync(_delayTime);

            if (!_botService.AreMultipleBotsLeft)
            {
                return new RoundResult
                {
                    FinalResult = _botService.GetBotResults(),
                    IsFinished = true,
                    History = new List<RoundPartialHistory>()
                };
            }

            var partialResults = await _botService.PlayBotMovesAsync(_delayTime, _roundNumber);

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = partialResults.ToList()
            };
        }

        public UserControl GetVisualisationUserControl(IConfigurable configurable)
        {
            _delayTime = configurable.NextMoveDelay;
            return new TankBlasterUserControl(_field);
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            do
            {
                Reset();

                _field.GenerateRandomBoard();

                _botService.SetUpBots(competitors);

                _field.OnArenaChanged();

            } while (!_locationService.IsPassageBetweenTwoPlayers());
        }

        public void Reset()
        {
            _field.Reset();
            _roundNumber = 0;
        }

        public void SetPreview(object boardState)
        {
            _field.ImportState((Battlefield)boardState);
            _field.OnArenaChanged();
        }

        public string GetGameRules()
        {
            Resources.indestructibleTile.Save("hello_wars_indestructible_tile.png");
            Resources.regularTile.Save("hello_wars_regular_tile.png");
            Resources.fortifiedTile.Save("hello_wars_fortified_tile.png");
            Resources.bomb.Save("hello_wars_bomb.png");
            Resources.missile.Save("hello_wars_missile.png");
            Resources.hello_wars_example.Save("hello_wars_example.png");
            Resources.tank1.Save("hello_wars_tank.png");
            Resources.blast_example.Save("hello_wars_blast_example.png");
            return Resources.GameRules;
        }

        public void ApplyConfiguration(string configurationXml)
        {
            _gameConfig = new XmlSerializer<TankBlasterConfig>().Deserialize(configurationXml);
            _field = new Battlefield(_gameConfig.MapWidth, _gameConfig.MapHeight);
            _locationService = new LocationService(_field);
            _explosionService = new ExplosionService(_field, _gameConfig, _locationService);
            _botService = new BotService(_field, _gameConfig, _locationService);
        }
    }
}