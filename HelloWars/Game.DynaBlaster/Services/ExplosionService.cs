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
        private readonly GameArena _arena;
        private readonly DynaBlasterConfig _gameConfig;
        private readonly LocationService _locationService;

        public ExplosionService(GameArena arena, DynaBlasterConfig config, LocationService locationService)
        {
            _arena = arena;
            _locationService = locationService;
            _gameConfig = config;
        }

        public async Task HandleExplodablesAsync(int explosionDisplayTime)
        {
            _arena.Explosions.Clear();

            foreach (var bomb in _arena.Bombs)
            {
                bomb.RoundsUntilExplodes--;
                if (!bomb.IsExploded && bomb.RoundsUntilExplodes == 0)
                {
                    SetExplosion(bomb);
                }
            }

            foreach (var missile in _arena.Missiles)
            {
                HandleMissileMovement(missile);
            }

            _arena.Bombs.RemoveAll(bomb => bomb.IsExploded);
            _arena.Missiles.RemoveAll(missile => missile.IsExploded);

            _arena.OnArenaChanged();

            await DelayHelper.DelayAsync(explosionDisplayTime);

            _arena.Explosions.Clear();
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

                if (_arena.Board[currentPoint.X, currentPoint.Y] != BoardTile.Empty)
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

                if (_locationService.IsLocationAvailableForMissile(newLocation) && _arena.Bots.All(bot => bot.Location != missile.Location))
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
            foreach (var bomb in _arena.Bombs)
            {
                if (explosion.BlastLocations.Any(point => point == bomb.Location) && !bomb.IsExploded)
                {
                    SetExplosion(bomb);
                }
            }

            foreach (var missile in _arena.Missiles)
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

            _arena.Explosions.Add(explosion);

            explodable.IsExploded = true;

            foreach (var explosionLocation in explosion.BlastLocations)
            {
                switch (_arena.Board[explosionLocation.X, explosionLocation.Y])
                {
                    case BoardTile.Regular:
                        _arena.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Empty;
                        break;
                    case BoardTile.Fortified:
                        _arena.Board[explosionLocation.X, explosionLocation.Y] = BoardTile.Regular;
                        break;
                }

                _arena.Bots.ForEach(bot =>
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