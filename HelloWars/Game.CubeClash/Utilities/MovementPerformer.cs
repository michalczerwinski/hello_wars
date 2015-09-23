using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Common.Helpers;
using Game.AntWars.Enums;
using Game.AntWars.Models;
using Game.AntWars.Properties;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Utilities
{
    public class MovementPerformer
    {
        private static ImageSource _explosionImage = ResourceImageHelper.LoadImage(Resources.explosion);

        private AntWarsViewModel _antWarsViewModel;

        public MovementPerformer(AntWarsViewModel antWarsViewModel)
        {
            _antWarsViewModel = antWarsViewModel;

        }

        public void KillExplosions()
        {
            var explosionsToDelete = _antWarsViewModel.BattlefieldObjectsCollection.Where(t => t.Type == UnmovableObjectTypes.Explosion).ToList();

            foreach (var explosion in explosionsToDelete)
            {
                _antWarsViewModel.BattlefieldObjectsCollection.Remove(explosion);
            }
        }

        public async Task PerformAntMoveAsync()
        {
            var listOfMissilesToFire = new List<MissileModel>();
            var listOfAntsToRemove = new List<AntModel>();

            foreach (var movableObject in _antWarsViewModel.MovableObjectsCollection.OfType<AntModel>())
            {
                var move = await movableObject.NextMoveAsync(null);

                switch (move.Action)
                {
                    case AvailableActions.Move:
                        {
                            switch (move.ActionDirection)
                            {
                                case ActionDirections.Down:
                                    {
                                        if ((movableObject.Y < _antWarsViewModel.RowCount - 1) && (IfMoveIsValid(movableObject.X, movableObject.Y + 1)))
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
                                        if ((movableObject.X < _antWarsViewModel.ColumnCount - 1) && (IfMoveIsValid(movableObject.X + 1, movableObject.Y)))
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
                //if missile meet some ant
                if (_antWarsViewModel.MovableObjectsCollection.OfType<MissileModel>().Any(missile => missile.X == movableObject.X && missile.Y == movableObject.Y))
                {
                    listOfAntsToRemove.Add(movableObject);
                }
            }

            foreach (var ant in listOfAntsToRemove)
            {
                _antWarsViewModel.MovableObjectsCollection.Remove(ant);
            }

            foreach (var missile in listOfMissilesToFire)
            {
                _antWarsViewModel.MovableObjectsCollection.Add(missile);
            }

            if (_antWarsViewModel.MovableObjectsCollection.OfType<AntModel>().Count() <= 1)
            {
            }
        }

        public bool IfMoveIsValid(int newPointX, int newPointY)
        {
            if (_antWarsViewModel.MovableObjectsCollection.Any(ant => ant.X == newPointX && ant.Y == newPointY))
            {
                return false;
            }

            if (_antWarsViewModel.BattlefieldObjectsCollection.Where(type => type.Type == UnmovableObjectTypes.Wood || type.Type == UnmovableObjectTypes.Rock).Any(ant => ant.X == newPointX && ant.Y == newPointY))
            {
                return false;
            }

            return true;
        }

        public void PerformMissilesMove()
        {
            var listOfMissilesToRemove = new List<MissileModel>();

            foreach (var missileObject in _antWarsViewModel.MovableObjectsCollection.OfType<MissileModel>())
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

                    _antWarsViewModel.BattlefieldObjectsCollection.Add(explosion);
                    listOfMissilesToRemove.Add(missileObject);
                }

                //if missile meet some missile
                if (_antWarsViewModel.MovableObjectsCollection.OfType<MissileModel>().Any(missile => missile.X == missileObject.X && missile.Y == missileObject.Y))
                {
                    //detonate missileObject and make chain reaction if there are some missiles near
                }
            }

            foreach (var item in listOfMissilesToRemove)
            {
                _antWarsViewModel.MovableObjectsCollection.Remove(item);
            }
        }
    }
}
