using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            var listOfCubesToRemove = new List<CubeModel>();


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
                                        if ((movableObject.Y < _cubeClashViewModel.BattlegroundHeigth) && (IfMoveIsValid(movableObject.X, movableObject.Y + 1)))
                                        {
                                            movableObject.Down();
                                        }
                                        break;
                                    }
                                case ActionDirections.Up:
                                    {
                                        if ((movableObject.Y > 0) && (IfMoveIsValid(movableObject.X, movableObject.Y - 1)))
                                        {
                                            movableObject.Up();
                                        }
                                        break;
                                    }
                                case ActionDirections.Left:
                                    {
                                        if ((movableObject.X > 0) && (IfMoveIsValid(movableObject.X - 1, movableObject.Y)))
                                        {
                                            movableObject.Left();
                                        }
                                        break;
                                    }
                                case ActionDirections.Right:
                                    {
                                        if ((movableObject.X < _cubeClashViewModel.BattlegroundWidth) && (IfMoveIsValid(movableObject.X + 1, movableObject.Y)))
                                        {
                                            movableObject.Right();
                                        }
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
                //if missile meet some cube
                if (_cubeClashViewModel.MovableObjectsCollection.OfType<MissileModel>().Any(missile => missile.X == movableObject.X && missile.Y == movableObject.Y))
                {
                    listOfCubesToRemove.Add(movableObject);
                }
            }

            foreach (var cube in listOfCubesToRemove)
            {
                _cubeClashViewModel.MovableObjectsCollection.Remove(cube);
            }

            foreach (var missile in listOfMissilesToFire)
            {
                _cubeClashViewModel.MovableObjectsCollection.Add(missile);
            }



            if (_cubeClashViewModel.MovableObjectsCollection.OfType<CubeModel>().Count() <= 1)
            {


            }

        }

        private bool IfMoveIsValid(int newPointX, int newPointY)
        {
            if (_cubeClashViewModel.MovableObjectsCollection.Any(cube => cube.X == newPointX && cube.Y == newPointY))
            {
                return false;
            }
            // if (_cubeClashViewModel.BattlefieldObjectsCollection.Where(f=>f.))


            return true;
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

                //if missile meet some missile
                if (_cubeClashViewModel.MovableObjectsCollection.OfType<MissileModel>().Any(missile => missile.X == missileObject.X && missile.Y == missileObject.Y))
                {
                    //detonate missileObject and make chain reaction if there are some missiles near
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

            _cubeClashViewModel.RowCount = 20;
            _cubeClashViewModel.ColumnCount = 20;

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
                        Y = j,
                        Type = EnumValueHelper<UnmovableObjectTypes>.RandomEnumValue()
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
