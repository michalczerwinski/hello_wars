using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.Models;
using Game.CubeClash.UserControls;
using Game.CubeClash.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace Game.CubeClash
{
    public class CubeClash : IGame
    {
        private CubeClashViewModel _cubeClashViewModel;
        private IEnumerable<ICompetitor> _competitors;
        private Random _rand = new Random(DateTime.Now.Millisecond);

        public RoundResult PerformNextRound()
        {
            PerformMisslesMove();

            DelayHelper.Delay(100);

            PerformCubesMove();

            DelayHelper.Delay(100);

            return new RoundResult
            {
                FinalResult = null,
                IsFinished = false,
                History = new List<RoundPartialHistory>()
            };
        }

        private void PerformCubesMove()
        {
            var listOfMissilesToFire = new List<MissileModel>();

            foreach (var movableObject in _cubeClashViewModel.MovableObjectsCollection.OfType<CubeModel>())
            {
                var move = movableObject.NextMove(null);

                switch (move.AvailableActions)
                {
                    case AvailableActions.Move:
                        {
                            switch (move.ActionDirections)
                            {
                                case ActionDirections.Down:
                                    {
                                        movableObject.Down();
                                        break;
                                    }
                                case ActionDirections.Up:
                                    {
                                        movableObject.Up();
                                        break;
                                    }
                                case ActionDirections.Left:
                                    {
                                        movableObject.Left();
                                        break;
                                    }
                                case ActionDirections.Right:
                                    {
                                        movableObject.Right();
                                        break;
                                    }
                            }
                            break;
                        }
                    case AvailableActions.Watch:
                        {
                            movableObject.Watch();
                            break;
                        }
                    case AvailableActions.FireMissile:
                        {
                            listOfMissilesToFire.Add(MissileToLounch(movableObject.X, movableObject.Y, move.ActionDirections));
                            break;
                        }
                }
            }

            foreach (var item in listOfMissilesToFire)
            {
                _cubeClashViewModel.MovableObjectsCollection.Add(item);
            }
        }

        private void PerformMisslesMove()
        {
            var listOfMissilesToRemove = new List<MissileModel>();

            foreach (var missileObject in _cubeClashViewModel.MovableObjectsCollection.OfType<MissileModel>())
            {
                switch (missileObject.Direction)
                {
                    case ActionDirections.Up:
                        {
                            missileObject.Y -= 1;
                            break;
                        }
                    case ActionDirections.Left:
                        {
                            missileObject.X -= 1;
                            break;
                        }
                    case ActionDirections.Down:
                        {
                            missileObject.Y += 1;
                            break;
                        }
                    case ActionDirections.Right:
                        {
                            missileObject.X += 1;
                            break;
                        }
                }
                missileObject.Range--;
                if (missileObject.Range < 0)
                {
                    listOfMissilesToRemove.Add(missileObject);
                }
            }

            foreach (var item in listOfMissilesToRemove)
            {
                _cubeClashViewModel.MovableObjectsCollection.Remove(item);
            }
        }

        private MissileModel MissileToLounch(int x, int y, ActionDirections actionDirection)
        {
            var missile = new MissileModel(new MissileViewModel())
            {
                X = x,
                Y = y,
                Range = 10,
                Direction = actionDirection,
            };

            switch (actionDirection)
            {
                case ActionDirections.Up:
                    {
                        missile.Y -= 1;
                        break;
                    }
                case ActionDirections.Left:
                    {
                        missile.X -= 1;
                        break;
                    }
                case ActionDirections.Down:
                    {
                        missile.Y += 1;
                        break;
                    }
                case ActionDirections.Right:
                    {
                        missile.X += 1;
                        break;
                    }
            }

            return missile;
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

                _cubeClashViewModel.MovableObjectsCollection.Add(cubeModel);
            }
        }

        private void InitiateBoardSizes()
        {
            _cubeClashViewModel.CubeHeigth = 10;
            _cubeClashViewModel.CubeWidth = 10;

            _cubeClashViewModel.RowCount = 40;
            _cubeClashViewModel.ColumnCount = 40;

            _cubeClashViewModel.BattlegroundWidth = _cubeClashViewModel.RowCount * _cubeClashViewModel.CubeHeigth;
            _cubeClashViewModel.BattlegroundHeigth = _cubeClashViewModel.ColumnCount * _cubeClashViewModel.CubeWidth;
        }

        private void InitiateBattlefield()
        {
            _cubeClashViewModel.BattlefieldObjectsCollection = new ObservableCollection<IUnmovableObject>();

            for (int i = 0; i < _cubeClashViewModel.RowCount; i++)
            {
                for (int j = 0; j < _cubeClashViewModel.ColumnCount; j++)
                {
                    var gridUnit = new GridUnitModel(new GridUnitViewModel())
                    {
                        X = i,
                        Y = j
                    };

                    _cubeClashViewModel.BattlefieldObjectsCollection.Add(gridUnit);
                }
            }
        }

        public void Reset()
        {
            _cubeClashViewModel.MovableObjectsCollection.Clear();
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
            _cubeClashViewModel.MovableObjectsCollection = new ObservableCollection<IMovableObject>();
        }

        public string GetGameRules()
        {
            throw new NotImplementedException();
        }


        public void ApplyConfiguration(string configurationXml)
        {
            throw new NotImplementedException();
        }
    }
}
