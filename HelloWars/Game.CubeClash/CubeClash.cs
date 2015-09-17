using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Interfaces;
using Game.CubeClash.Models;
using Game.CubeClash.UserControls;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash
{
    public class CubeClash : IGame
    {
        private CubeClashViewModel _cubeClashViewModel;
        private IEnumerable<ICompetitor> _competitors; 


        public RoundResult PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            InitiateBoard();
            Reset();
            _competitors = competitors;
        }

        private void InitiateBoard()
        {
            _cubeClashViewModel.GridCollection = new ObservableCollection<IUnmovableObjeect>();

            _cubeClashViewModel.CubeHeigth = 10;
            _cubeClashViewModel.CubeWidth = 10;

            _cubeClashViewModel.RowCount = 40;
            _cubeClashViewModel.ColumnCount = 40;

            _cubeClashViewModel.BattlegroundWidth = _cubeClashViewModel.RowCount * _cubeClashViewModel.CubeHeigth;
            _cubeClashViewModel.BattlegroundHeigth = _cubeClashViewModel.ColumnCount * _cubeClashViewModel.CubeWidth;

            for (int i = 0; i < _cubeClashViewModel.RowCount; i++)
            {
                for (int j = 0; j < _cubeClashViewModel.ColumnCount; j++)
                {
                    var ccc = new GridUnitModel(new GridUnitViewModel());
                    ccc.X = i;
                    ccc.Y = j;

                    _cubeClashViewModel.GridCollection.Add(ccc);
                }
            }
        }

        public void Reset()
        {
            _cubeClashViewModel.PlayersCollection = new ObservableCollection<IMovableObiects>();

            for (int i = 0; i < 1; i++)
            {
                var cubeModel = new CubeModel(new CubeViewModel(), _cubeClashViewModel.CubeWidth, _cubeClashViewModel.CubeHeigth)
                {
                    X = 20,
                    Y = 20,
                    Color = new SolidColorBrush(Colors.BlueViolet)
                };

                _cubeClashViewModel.PlayersCollection.Add(cubeModel);
            }
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _cubeClashViewModel = new CubeClashViewModel();
            InitiateBoard();
            Reset();
            return new CubeClashUserControl(_cubeClashViewModel);
        }


        public string GetGameRules()
        {
            throw new NotImplementedException();
        }
    }
}
