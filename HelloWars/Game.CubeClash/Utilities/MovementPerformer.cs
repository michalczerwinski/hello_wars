using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Common.Helpers;
using Game.CubeClash.Enums;
using Game.CubeClash.Models;
using Game.CubeClash.Properties;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Utilities
{
    public class MovementPerformer
    {
        private static ImageSource _explosionImage = ResourceImageHelper.LoadImage(Resources.explosion);

        private CubeClashViewModel _cubeClashViewModel;

        public MovementPerformer(CubeClashViewModel cubeClashViewModel)
        {
            _cubeClashViewModel = cubeClashViewModel;

        }

        public void KillExplosions()
        {
            var explosionsToDelete = _cubeClashViewModel.BattlefieldObjectsCollection.Where(t => t.Type == UnmovableObjectTypes.Explosion).ToList();

            foreach (var explosion in explosionsToDelete)
            {
                _cubeClashViewModel.BattlefieldObjectsCollection.Remove(explosion);
            }
        }

        public void PerformCubesMove()
        {
            var listOfMissilesToFire = new List<MissileModel>();
            var listOfCubesToRemove = new List<CubeModel>();

            foreach (var movableObject in _cubeClashViewModel.MovableObjectsCollection.OfType<CubeModel>())
            {
                var move = movableObject.NextMove(null);

                switch (move.Action)
                {
                    case AvailableActions.Move:
                        {
                            switch (move.ActionDirection)
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
                            listOfMissilesToFire.Add(movableObject.FireMissile(move.ActionDirection));
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

        public bool IfMoveIsValid(int newPointX, int newPointY)
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

        public void PerformMissilesMove()
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
                    var explosion = new ExplosionModel(new ExplosionViewModel())
                    {
                        X = missileObject.X,
                        Y = missileObject.Y,
                        ViewModel =
                        {
                            Image = _explosionImage,
                        }
                    };

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
    }
}
