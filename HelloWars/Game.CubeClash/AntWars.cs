using System;
using System.Collections.Generic;
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

        private MovementPerformer _movementPerformer;
        #region IGameMembers

        public RoundResult PerformNextRound()
        {
            _movementPerformer.KillExplosions();
            DelayHelper.Delay(20);

            _movementPerformer.PerformMissilesMove();

            DelayHelper.Delay(100);

           _movementPerformer.PerformAntMove();

            DelayHelper.Delay(100);

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
            Reset();
            BotBuilder.AddBots(competitors, _antWarsViewModel);
        }

        public void Reset()
        {
            _antWarsViewModel.MovableObjectsCollection.Clear();
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _antWarsViewModel = new AntWarsViewModel();
            _movementPerformer = new MovementPerformer(_antWarsViewModel);

            BattlegroundBuilder.CreateBattleground(10, 10, _antWarsViewModel);
            return new AntWarsUserControl(_antWarsViewModel);
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
