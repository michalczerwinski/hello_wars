using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Interfaces;
using Common.Models;
using Common.Serialization;
using Game.DynaBlaster.Models;
using Game.DynaBlaster.Properties;
using Game.DynaBlaster.Services;
using Game.DynaBlaster.UserControls;

namespace Game.DynaBlaster
{
    public class DynaBlaster : IGame
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private Battlefield _field;
        private BotService _botService;
        private int _delayTime;
        private ExplosionService _explosionService;
        private DynaBlasterConfig _gameConfig;
        private LocationService _locationService;
        private int _roundNumber;

        public DynaBlaster()
        {
            var xmlStream = new StreamReader("DynaBlaster.config.xml");
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
            return new DynaBlasterUserControl(_field);
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            Reset();

            _field.GenerateRandomBoard();

            _botService.SetUpBots(competitors);

            _field.OnArenaChanged();
        }

        public void Reset()
        {
            _field.Reset();
            _roundNumber = 0;
        }

        public void SetPreview(object boardState)
        {
            _field.ImportState((Battlefield) boardState);
            _field.OnArenaChanged();
        }

        public string GetGameRules()
        {
            return Resources.GameRules;
        }

        public void ApplyConfiguration(string configurationXml)
        {
            _gameConfig = new XmlSerializer<DynaBlasterConfig>().Deserialize(configurationXml);
            _field = new Battlefield(_gameConfig.MapWidth, _gameConfig.MapHeight);
            _locationService = new LocationService(_field);
            _explosionService = new ExplosionService(_field, _gameConfig, _locationService);
            _botService = new BotService(_field, _gameConfig, _locationService);
        }
    }
}