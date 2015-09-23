﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.AntWars.UserControls;
using Game.AntWars.Utilities;
using Game.AntWars.ViewModels;

namespace Game.AntWars
{
    public class AntWars : IGame
    {
        private AntWarsViewModel _antWarsViewModel;
        private MovementService _movementService;

        #region IGameMembers

        public async Task<RoundResult> PerformNextRoundAsync()
        {
            _movementService.ExpiryExplosions();
            DelayHelper.Delay(10);

            _movementService.PerformMissilesMove();
            DelayHelper.Delay(100);

           await _movementService.PerformAntMoveAsync();

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = new List<RoundPartialHistory>()
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
            BattlegroundBuilder.CreateBattleground(20, 20, _antWarsViewModel);
            BotBuilder.AddBots(competitors, _antWarsViewModel);
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
            return new AntWarsUserControl(_antWarsViewModel);
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
