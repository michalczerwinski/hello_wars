using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Enums;
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
        private Random _rand = new Random(DateTime.Now.Millisecond);

        public RoundResult PerformNextRound()
        {
            foreach (var competitor in _competitors)
            {
                var cubeModel = _cubeClashViewModel.PlayersCollection.First(bot => (bot as CubeModel).Competitor.Id == competitor.Id) as CubeModel;

                if (cubeModel != null)
                {
                    var move = cubeModel.NextMove(null);

                    switch (move.Move)
                    {
                        case AvailableMoves.Down:
                            {
                                cubeModel.Down();
                                break;
                            }
                        case AvailableMoves.Up:
                            {
                                cubeModel.Up();
                                break;
                            }
                        case AvailableMoves.Left:
                            {
                                cubeModel.Left();
                                break;
                            }
                        case AvailableMoves.Right:
                            {
                                cubeModel.Right();
                                break;
                            }
                        case AvailableMoves.Attack:
                            {
                                cubeModel.Attack();
                                break;
                            }
                    }
                }
            }

            DelayHelper.Delay(100);


            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = new List<RoundPartialHistory>()
            };
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            Reset();
            _competitors = competitors;
            AddCompetitors();
        }

        private void AddCompetitors()
        {
            foreach (var competitor in _competitors)
            {
                var cubeModel = new CubeModel(new CubeViewModel(), competitor, _cubeClashViewModel.CubeWidth, _cubeClashViewModel.CubeHeigth)
                {
                    X = _rand.Next(0, _cubeClashViewModel.ColumnCount),
                    Y = _rand.Next(0, _cubeClashViewModel.RowCount),
                    Color = new SolidColorBrush(Colors.BlueViolet)
                };

                _cubeClashViewModel.PlayersCollection.Add(cubeModel);
            }
        }

        private void InitiateBoardSizes()
        {
            _cubeClashViewModel.CubeHeigth = 10;
            _cubeClashViewModel.CubeWidth = 10;

            _cubeClashViewModel.RowCount =30;
            _cubeClashViewModel.ColumnCount = 30;

            _cubeClashViewModel.BattlegroundWidth = _cubeClashViewModel.RowCount * _cubeClashViewModel.CubeHeigth;
            _cubeClashViewModel.BattlegroundHeigth = _cubeClashViewModel.ColumnCount * _cubeClashViewModel.CubeWidth;
        }

        private void InitiateBattlefield()
        {
            _cubeClashViewModel.GridCollection = new ObservableCollection<IUnmovableObjeect>();

            for (int i = 0; i < _cubeClashViewModel.RowCount; i++)
            {
                for (int j = 0; j < _cubeClashViewModel.ColumnCount; j++)
                {
                    var gridUnit = new GridUnitModel(new GridUnitViewModel())
                    {
                        X = i,
                        Y = j
                    };

                    _cubeClashViewModel.GridCollection.Add(gridUnit);
                }
            }
        }

        public void Reset()
        {
            _cubeClashViewModel.PlayersCollection.Clear();
        }

        public void SetPreview(object boardState)
        {
            throw new NotImplementedException();
        }

        public UserControl GetVisualisationUserControl(IConfigurable configuration)
        {
            _cubeClashViewModel = new CubeClashViewModel();
            InitiateBoardSizes();
            InitiatePlayersCollection();
            InitiateBattlefield();
            return new CubeClashUserControl(_cubeClashViewModel);
        }

        private void InitiatePlayersCollection()
        {
            _cubeClashViewModel.PlayersCollection = new ObservableCollection<IMovableObiects>();
        }

        public string GetGameRules()
        {
            throw new NotImplementedException();
        }
    }
}
