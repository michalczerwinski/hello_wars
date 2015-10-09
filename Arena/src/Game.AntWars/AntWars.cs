using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.AntWars.Models.BaseUnits;
using Game.AntWars.Services;
using Game.AntWars.UserControls;
using Game.AntWars.ViewModels;
using Game.AntWars.ViewModels.BaseUnits;

namespace Game.AntWars
{
    public class AntWars : IGame
    {
        private AntWarsViewModel _antWarsViewModel;
        private MovementService _movementService;
        private BotService _botService;
        private int _roundNumber;

        #region IGameMembers

        public async Task<RoundResult> PerformNextRoundAsync()
        {
            _roundNumber++;

            _movementService.ExpiryExplosions();
            await DelayHelper.DelayAsync(50);

            _movementService.PerformMissilesMove();
            await DelayHelper.DelayAsync(150);

            //call winner
            if (_antWarsViewModel.MovableObjectsCollection.OfType<AntModel>().Count() <= 1)
            {
                return new RoundResult
                {
                    FinalResult = _botService.GetBotResults(),
                    IsFinished = true,
                    History = new List<RoundPartialHistory>()
                };
            }

            var partialResults = await _movementService.PlayAntsMoveAsync(200, _roundNumber);

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = partialResults,
            };
        }

        public string GetGameRules()
        {
            throw new NotImplementedException();
        }

        public void ApplyConfiguration(string configurationXml)
        {
            throw new NotImplementedException();
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            _antWarsViewModel.SplashScreenVisible = Visibility.Collapsed;
            Reset();
            BattlegroundService.CreateBattleground(20, 20, _antWarsViewModel);
            _botService.AddBots(competitors);
        }

        public void Reset()
        {
            _antWarsViewModel.MovableObjectsCollection.Clear();
            _antWarsViewModel.BattlefieldObjectsCollection.Clear();

            AntViewModel.IsRedAntAdded = false;
            AntViewModel.IsYellowAntAdded = false;
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _antWarsViewModel = new AntWarsViewModel();
            _movementService = new MovementService(_antWarsViewModel);
            _botService = new BotService(_antWarsViewModel);
            return new AntWarsUserControl(_antWarsViewModel);
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
