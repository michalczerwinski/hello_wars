using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers;
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.Services
{
    internal class ExplosionService
    {
        private readonly Battlefield _field;
        private readonly DynaBlasterConfig _gameConfig;
        private readonly LocationService _locationService;

        public ExplosionService(Battlefield field, DynaBlasterConfig config, LocationService locationService)
        {
            _field = field;
            _locationService = locationService;
            _gameConfig = config;
        }

        public async Task HandleExplodablesAsync(int explosionDisplayTime)
        {
            _field.Explosions.Clear();

            foreach (var bomb in _field.Bombs)
            {
                bomb.RoundsUntilExplodes--;
                if (!bomb.IsExploded && bomb.RoundsUntilExplodes == 0)
                {
                    SetExplosion(bomb);
                }
            }

            foreach (var missile in _field.Missiles)
            {
                HandleMissileMovement(missile);
            }

            _field.Bombs.RemoveAll(bomb => bomb.IsExploded);
            _field.Missiles.RemoveAll(missile => missile.IsExploded);

            _field.OnArenaChanged();

            await DelayHelper.DelayAsync(explosionDisplayTime);

            _field.Explosions.Clear();
        }

        private IEnumerable<Point> CalculateExplosionRay(ExplodableBase explodable, MoveDirection direction)
        {
            var currentPoint = explodable.Location;
            for (var i = 1; i <= explodable.ExplosionRadius; i++)
            {
                currentPoint = _locationService.GetNewLocation(currentPoint, direction);

                if (!_locationService.IsLocationValid(currentPoint))
                {
                    yield break;
                }

                yield return currentPoint;

                if (_field.Board[currentPoint.X, currentPoint.Y] != BoardTile.Empty)
                {
                    yield break;
                }
            }
        }

        private IEnumerable<Point> GetExplosionLocations(ExplodableBase explodable)
        {
            var result = new List<Point> {explodable.Location};

            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Up));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Down));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Right));
            result.AddRange(CalculateExplosionRay(explodable, MoveDirection.Left));

            return result;
        }

        private void HandleMissileMovement(Missile missile)
        {
            if (!missile.IsExploded)
            {
                var newLocation = _locationService.GetNewLocation(missile.Location, missile.MoveDirection);

                if (_locationService.IsLocationAvailableForMissile(newLocation) && _field.Bots.All(bot => bot.Location != missile.Location))
                {
                    missile.Location = newLocation;

                    if (!_gameConfig.IsFastMissileModeEnabled || _locationService.IsLocationAvailableForMissile(_locationService.GetNewLocation(newLocation, missile.MoveDirection)))
                    {
                        return;
                    }
                }

                SetExplosion(missile);
            }
        }

        private void SetChainedExplosions(Explosion explosion)
        {
            foreach (var bomb in _field.Bombs)
            {
                if (explosion.BlastLocations.Any(point => point == bomb.Location) && !bomb.IsExploded)
                {
                    SetExplosion(bomb);
                }
            }

            foreach (var missile in _field.Missiles)
            {
                if (explosion.BlastLocations.Any(point => point == missile.Location) && !missile.IsExploded)
                {
                    SetExplosion(missile);
                }
            }
        }

        private void SetExplosion(ExplodableBase explodable)
        {
            var explosion = new Explosion
            {
                Center = explodable.Location,
                BlastLocations = GetExplosionLocations(explodable)
            };

            _field.Explosions.Add(explosion);

            explodable.IsExploded = true;

            foreach (var explosionLocation in explosion.BlastLocations)
            {
                switch (_field.Board[explosionLocation.X, explosionLocation.Y])
                {
                    case BoardTile.Regular:
                        _field.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Empty;
                        break;
                    case BoardTile.Fortified:
                        _field.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Regular;
                        break;
                }

                _field.Bots.ForEach(bot =>
                {
                    if (bot.Location == explosionLocation)
                    {
                        bot.IsDead = true;
                    }
                });
            }

            SetChainedExplosions(explosion);
        }
    }
}