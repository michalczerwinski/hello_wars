using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.Models;
using Game.CubeClash.Properties;
using Game.CubeClash.UserControls;
using Game.CubeClash.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace Game.CubeClash
{
    public class CubeClash : IGame
    {
        private static ImageSource _redAntImage = ResourceImageHelper.LoadImage(Resources.redAnt);
        private bool _isRedAntAdded = false;
        private static ImageSource _yellowAntImage = ResourceImageHelper.LoadImage(Resources.yellowAnt);
        private bool _isYellowAntAdded = false;

        private static ImageSource _yellowMissileImage = ResourceImageHelper.LoadImage(Resources.yellowMissile);
        private static ImageSource _redMissileImage = ResourceImageHelper.LoadImage(Resources.redMissile);
        private static ImageSource _explosionImage = ResourceImageHelper.LoadImage(Resources.explosion);


        private CubeClashViewModel _cubeClashViewModel;
        private IEnumerable<ICompetitor> _competitors;
        private Random _rand = new Random(DateTime.Now.Millisecond);

        public RoundResult PerformNextRound()
        {
            KillExplosions();
            DelayHelper.Delay(20);

            PerformMissilesMove();

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

        private void KillExplosions()
        {
            var explosionsToDelete = _cubeClashViewModel.BattlefieldObjectsCollection.Where(t => t.Type == UnmovableObjectTypes.Explosion).ToList();

            foreach (var explosion in explosionsToDelete)
            {
                _cubeClashViewModel.BattlefieldObjectsCollection.Remove(explosion);
            }
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
                                        if ((movableObject.Y < _cubeClashViewModel.RowCount - 1) && (IfMoveIsValid(movableObject.X, movableObject.Y + 1)))
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
                                        if ((movableObject.X < _cubeClashViewModel.ColumnCount - 1) && (IfMoveIsValid(movableObject.X + 1, movableObject.Y)))
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
                            // movableObject.Watch();
                            break;
                        }
                    case AvailableActions.FireMissile:
                        {
                            listOfMissilesToFire.Add(MissileToLounch(movableObject.X, movableObject.Y, move.ActionDirections, movableObject.ViewModel.Image));
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
            if (_cubeClashViewModel.BattlefieldObjectsCollection.Where(type => type.Type == UnmovableObjectTypes.Wood || type.Type == UnmovableObjectTypes.Rock).Any(cube => cube.X == newPointX && cube.Y == newPointY))
            {
                return false;
            }

            return true;
        }

        private void PerformMissilesMove()
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
                    var explosion = new ExplosionViewModel();
                    explosion.X = missileObject.X;
                    explosion.Y = missileObject.Y;
                    explosion.Image = _explosionImage;
                    explosion.Width = explosion.Heigth = 30;

                    _cubeClashViewModel.BattlefieldObjectsCollection.Add(explosion);
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

        private MissileModel MissileToLounch(int x, int y, ActionDirections actionDirection, ImageSource imageSource)
        {
            var missile = new MissileModel(new MissileViewModel())
            {
                X = x,
                Y = y,
                Range = 5,
                Direction = actionDirection,
            };

            missile.ViewModel.Width = _cubeClashViewModel.CubeWidth;
            missile.ViewModel.Heigth = _cubeClashViewModel.CubeHeigth;

            if (imageSource == _redAntImage)
            {
                missile.ViewModel.Image = _redMissileImage;
            }
            else if (imageSource == _yellowAntImage)
            {
                missile.ViewModel.Image = _yellowMissileImage;
            }

            switch (actionDirection)
            {
                case ActionDirections.Up:
                    {
                        missile.Y -= 1;
                        missile.ViewModel.Angle = 270;
                        break;
                    }
                case ActionDirections.Left:
                    {
                        missile.X -= 1;
                        missile.ViewModel.Angle = 180;
                        break;
                    }
                case ActionDirections.Down:
                    {
                        missile.Y += 1;
                        missile.ViewModel.Angle = 90;
                        break;
                    }
                case ActionDirections.Right:
                    {
                        missile.X += 1;
                        missile.ViewModel.Angle = 0;
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
                var viewModel = new CubeViewModel();
                viewModel.Width = _cubeClashViewModel.CubeWidth;
                viewModel.Heigth = _cubeClashViewModel.CubeHeigth;

                if (!_isRedAntAdded)
                {
                    viewModel.Image = _redAntImage;
                    _isRedAntAdded = true;
                }
                else if (!_isYellowAntAdded)
                {
                    viewModel.Image = _yellowAntImage;
                    _isYellowAntAdded = true;
                }

                var cubeModel = new CubeModel(viewModel, competitor, _cubeClashViewModel.CubeWidth, _cubeClashViewModel.CubeHeigth)
                {
                    X = _rand.Next(0, _cubeClashViewModel.ColumnCount),
                    Y = _rand.Next(0, _cubeClashViewModel.RowCount),
                };

                _cubeClashViewModel.MovableObjectsCollection.Add(cubeModel);
            }
        }

        private void InitiateBoardSizes()
        {
            _cubeClashViewModel.CubeHeigth = 10;
            _cubeClashViewModel.CubeWidth = 10;

            _cubeClashViewModel.RowCount = 10;
            _cubeClashViewModel.ColumnCount = 10;

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
                        Type = ReturnWithSomeProbability()
                    };

                    _cubeClashViewModel.BattlefieldObjectsCollection.Add(gridUnit);
                }
            }
        }

        private UnmovableObjectTypes ReturnWithSomeProbability()
        {
            var probability = _rand.NextDouble();

            if (probability < 0.15)
            {
                return UnmovableObjectTypes.Wood;
            }
            if (probability < 0.3)
            {
                return UnmovableObjectTypes.Rock;
            }
            return UnmovableObjectTypes.Lawn;
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
