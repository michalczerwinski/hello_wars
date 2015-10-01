using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Common.Helpers;
using Common.Models;
using Game.AntWars.Enums;
using Game.AntWars.Models.BaseUnits;
using Game.AntWars.Properties;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Services
{
    public class MovementService
    {
        private static ImageSource _explosionImage = ResourceImageHelper.LoadImage(Resources.kaboom);
        private AntWarsViewModel _antWarsViewModel;
        private IList<MissileModel> _listOfMissilesToFire;
        private IList<AntModel> _listOfAntsToRemove;

        public MovementService(AntWarsViewModel antWarsViewModel)
        {
            _antWarsViewModel = antWarsViewModel;
            _listOfMissilesToFire = new List<MissileModel>();
        }

        public async Task<List<RoundPartialHistory>> PlayAntsMoveAsync(int delayBetweenBots, int roundnumber)
        {
            var result = new List<RoundPartialHistory>();
            _listOfMissilesToFire.Clear();

            foreach (var movableObject in _antWarsViewModel.MovableObjectsCollection.OfType<AntModel>())
            {
                result.Add(await PerformMove(movableObject, roundnumber));
                await DelayHelper.DelayAsync(delayBetweenBots);
            }

            //fire missile
            foreach (var missile in _listOfMissilesToFire)
            {
                _antWarsViewModel.MovableObjectsCollection.Add(missile);
            }

            return result;
        }

        public void PerformMissilesMove()
        {
            _listOfAntsToRemove = new List<AntModel>();

            foreach (var missileObject in _antWarsViewModel.MovableObjectsCollection.OfType<MissileModel>())
            {
                switch (missileObject.Direction)
                {
                    case ActionDirections.Up:
                        {
                            if ((missileObject.Y > 0) && (IsMoveValid(missileObject.X, missileObject.Y - 1)))
                            {
                                missileObject.Y -= 1;
                            }
                            else
                            {
                                TurnMissileIntoExplosions(missileObject);
                            }
                            break;
                        }
                    case ActionDirections.Left:
                        {
                            if ((missileObject.X > 0) && (IsMoveValid(missileObject.X - 1, missileObject.Y)))
                            {
                                missileObject.X -= 1;
                            }
                            else
                            {
                                TurnMissileIntoExplosions(missileObject);
                            }
                            break;
                        }
                    case ActionDirections.Down:
                        {
                            if ((missileObject.Y < _antWarsViewModel.RowCount - 1) && (IsMoveValid(missileObject.X, missileObject.Y + 1)))
                            {
                                missileObject.Y += 1;
                            }
                            else
                            {
                                TurnMissileIntoExplosions(missileObject);
                            }
                            break;
                        }
                    case ActionDirections.Right:
                        {
                            if ((missileObject.X < _antWarsViewModel.ColumnCount - 1) && (IsMoveValid(missileObject.X + 1, missileObject.Y)))
                            {
                                missileObject.X += 1;
                            }
                            else
                            {
                                TurnMissileIntoExplosions(missileObject);
                            }
                            break;
                        }
                }

                missileObject.Range--;

                if (missileObject.Range < 0)
                {
                    TurnMissileIntoExplosions(missileObject);
                }
            }

            RemoveDeadAnts();
        }

        private async Task<RoundPartialHistory> PerformMove(AntModel movableObject, int roundNumber)
        {
            var move = await movableObject.NextMoveAsync(null);
            var actionDescription = move.Action.ToString() + " ";

            if (move.ActionDirection != null)
            {
                actionDescription += move.ActionDirection.Value.ToString() + ". ";
            }

            switch (move.Action)
            {
                case AvailableActions.Move:
                    {
                        switch (move.ActionDirection)
                        {
                            case ActionDirections.Down:
                                {
                                    if (IsMoveValid(movableObject.X, movableObject.Y + 1))
                                    {
                                        movableObject.Down();
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Up:
                                {
                                    if (IsMoveValid(movableObject.X, movableObject.Y - 1))
                                    {
                                        movableObject.Up();
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Left:
                                {
                                    if (IsMoveValid(movableObject.X - 1, movableObject.Y))
                                    {
                                        movableObject.Left();
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Right:
                                {
                                    if (IsMoveValid(movableObject.X + 1, movableObject.Y))
                                    {
                                        movableObject.Right();
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
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

                        switch (move.ActionDirection)
                        {
                            case ActionDirections.Down:
                                {
                                    if (IsMoveValid(movableObject.X, movableObject.Y + 1))
                                    {
                                        _listOfMissilesToFire.Add(movableObject.FireMissile(move.ActionDirection.Value));
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Up:
                                {
                                    if (IsMoveValid(movableObject.X, movableObject.Y - 1))
                                    {
                                        _listOfMissilesToFire.Add(movableObject.FireMissile(move.ActionDirection.Value));
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Left:
                                {
                                    if (IsMoveValid(movableObject.X - 1, movableObject.Y))
                                    {
                                        _listOfMissilesToFire.Add(movableObject.FireMissile(move.ActionDirection.Value));
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                            case ActionDirections.Right:
                                {
                                    if (IsMoveValid(movableObject.X + 1, movableObject.Y))
                                    {
                                        _listOfMissilesToFire.Add(movableObject.FireMissile(move.ActionDirection.Value));
                                    }
                                    else
                                    {
                                        actionDescription += "Lost his move.";
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }

            return new RoundPartialHistory
            {
                Caption = string.Format("Round {0} {1}: {2}", roundNumber, movableObject.Competitor.Name, actionDescription),
            };
        }

        private bool IsMoveValid(int newX, int newY)
        {
            if (_antWarsViewModel.BattlefieldObjectsCollection.Where(type => type.Type == UnmovableObjectTypes.Wood || type.Type == UnmovableObjectTypes.Rock).Any(ant => ant.X == newX && ant.Y == newY))
            {
                return false;
            }

            if ((newY > _antWarsViewModel.RowCount - 1) || (newY < 0)
                || (newX > _antWarsViewModel.ColumnCount - 1) || (newX < 0))
            {
                return false;
            }

            if (_antWarsViewModel.MovableObjectsCollection.Any(ant => ant.X == newX && ant.Y == newY))
            {
                return false;
            }

            return true;
        }

        private void RemoveDeadAnts()
        {
            //remove dead Ants
            foreach (var ant in _listOfAntsToRemove)
            {
                _antWarsViewModel.MovableObjectsCollection.Remove(ant);
            }
        }

        public void ExpiryExplosions()
        {
            var explosionsToDelete = _antWarsViewModel.MovableObjectsCollection.Where(t => t.Type == MovableObjectsTypes.Explosion).ToList();

            foreach (var explosion in explosionsToDelete)
            {
                _antWarsViewModel.MovableObjectsCollection.Remove(explosion);
            }
        }

        private void TurnMissileIntoExplosions(MissileModel missile)
        {
            missile.ViewModel.Image = _explosionImage;
            missile.Type = MovableObjectsTypes.Explosion;

            var movableObjectsToDestroy = _antWarsViewModel.MovableObjectsCollection.
                Where(mo => mo.Type == MovableObjectsTypes.Bot || mo.Type == MovableObjectsTypes.Missile).
                Where(mo => mo.X >= missile.X - 1 && mo.X <= missile.X + 1 && mo.Y >= missile.Y - 1 && mo.Y <= missile.Y + 1).
                Where(mo => mo != missile).
                Where(mo => mo.Type != MovableObjectsTypes.Explosion).ToList();

            var unmovableObjectsToDestroy = _antWarsViewModel.BattlefieldObjectsCollection.
                Where(mo => mo.X >= missile.X - 1 && mo.X <= missile.X + 1 && mo.Y >= missile.Y - 1 && mo.Y <= missile.Y + 1).
                Where(mo => mo.Type == UnmovableObjectTypes.Wood).ToList();

            foreach (var item in movableObjectsToDestroy)
            {
                switch (item.Type)
                {
                    case MovableObjectsTypes.Bot:
                        {
                            _listOfAntsToRemove.Add((AntModel)item);
                            break;
                        }
                    case MovableObjectsTypes.Missile:
                        {
                            TurnMissileIntoExplosions((MissileModel)item);
                            break;
                        }
                }
            }

            foreach (var item in unmovableObjectsToDestroy)
            {
                if (item.Type == UnmovableObjectTypes.Wood)
                {
                    _antWarsViewModel.BattlefieldObjectsCollection.Remove(item);
                }
            }
        }
    }
}
