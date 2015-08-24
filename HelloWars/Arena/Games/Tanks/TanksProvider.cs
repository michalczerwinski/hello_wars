using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Arena.Interfaces;

namespace Arena.Games.Tanks
{
    class TanksProvider: IGameProvider
    {
        private TankGameViewModel _viewModel;

        public IGame CreateNewGame(IEnumerable<Arena.Models.Competitor> competitors)
        {
            throw new NotImplementedException();
        }

        public UserControl GetVisualisation()
        {
            _viewModel = new TankGameViewModel();
            SetBattleFieldSize(100, 100);
            return new TankGameControl(_viewModel);
        }

        private void SetBattleFieldSize(int width, int heigth)
        {
            _viewModel.Width = width;
            _viewModel.Heigth = heigth;
        }
    }
}
