using Game.Common.Models;

namespace Arena.Games.PickTheWinner.ViewModels
{
    public class TankViewModel : BindableBase
    {
        private int _turretAngle;
        private int _tankAngle;

        public int TurretAngle
        {
            get { return _turretAngle; }
            set { SetProperty(ref _turretAngle, value); }
        }

        public int TankAngle
        {
            get { return _tankAngle; }
            set { SetProperty(ref _tankAngle, value); }
        }
    }
}
