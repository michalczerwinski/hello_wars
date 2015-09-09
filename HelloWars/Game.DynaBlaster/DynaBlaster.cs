using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Interfaces;
using Common.Models;
using Game.DynaBlaster.Models;
using Game.DynaBlaster.UserControls;

namespace Game.DynaBlaster
{
    public class DynaBlaster : IGame
    {

        public RoundResult PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public UserControl GetVisualisationControl()
        {
            return new DynaBlasterUserControl(new DynaBlasterControlViewModel());
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
    }
}
