using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.UserControls;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash
{
    public class CubeClash : IGame
    {
        private CubeClashViewModel _cubeClashViewModel; 

        public RoundResult PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _cubeClashViewModel = new CubeClashViewModel();
            return new CubeClashUserControl(_cubeClashViewModel);
        }
    }
}
