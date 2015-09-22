using System;
using System.Collections.Generic;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.UserControls;
using Game.CubeClash.Utilities;
using Game.CubeClash.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace Game.CubeClash
{
    public class CubeClash : IGame
    {
        private CubeClashViewModel _cubeClashViewModel;

        private MovementPerformer _movementPerformer;
        #region IGameMembers

        public RoundResult PerformNextRound()
        {
            _movementPerformer.KillExplosions();
            DelayHelper.Delay(20);

            _movementPerformer.PerformMissilesMove();

            DelayHelper.Delay(100);

           _movementPerformer.PerformCubesMove();

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
            BotBuilder.AddBots(competitors, _cubeClashViewModel);
        }

        public void Reset()
        {
            _cubeClashViewModel.MovableObjectsCollection.Clear();
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _cubeClashViewModel = new CubeClashViewModel();
            _movementPerformer = new MovementPerformer(_cubeClashViewModel);

            BattlegroundBuilder.CreateBattleground(10, 10, _cubeClashViewModel);
            return new CubeClashUserControl(_cubeClashViewModel);
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
