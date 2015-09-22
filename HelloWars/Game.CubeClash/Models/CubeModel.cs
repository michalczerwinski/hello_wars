using System.Windows;
using System.Windows.Media;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.Properties;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Models
{
    public class CubeModel : BotClientBase<SurroundingAreaInfo, CubeMove>, IMovableObject
    {
        private static ImageSource _yellowMissileImage = ResourceImageHelper.LoadImage(Resources.yellowMissile);
        private static ImageSource _redMissileImage = ResourceImageHelper.LoadImage(Resources.redMissile);

        public ICompetitor Competitor { get; set; }
        public CubeViewModel ViewModel { get; set; }
        private readonly int _cubeWidth;
        private readonly int _cubeHeigth;

        public int X
        {
            get { return ViewModel.X; }
            set { ViewModel.X = value; }
        }

        public int Y
        {
            get { return ViewModel.Y; }
            set { ViewModel.Y = value; }
        }

        public SolidColorBrush Color
        {
            get { return ViewModel.Color; }
            set { ViewModel.Color = value; }
        }

        public CubeModel(CubeViewModel viewModel, ICompetitor competitor)
            : base(competitor)
        {
            Competitor = competitor;
            ViewModel = viewModel;
            InitiateMovementShadow();
        }

        #region actions

        public MissileModel FireMissile(ActionDirections actionDirection)
        {
            StopWatching();
            ViewModel.IsAttacking = "True";

            var missile = new MissileModel(new MissileViewModel())
            {
                X = this.X,
                Y = this.Y,
                Range = 6,
                Direction = actionDirection,
            };

            if (ViewModel.Image == CubeViewModel._redAntImage)
            {
                missile.ViewModel.Image = _redMissileImage;
            }
            else if (ViewModel.Image == CubeViewModel._yellowAntImage)
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

        public void Watch()
        {
            ViewModel.MovementShadowLeftDistance = -100;
            ViewModel.MovementShadowTopDistance = -100;
            ViewModel.MovementShadowHeight = 200;
            ViewModel.MovementShadowWidth = 200;
        }

        public void StopWatching()
        {
            InitiateMovementShadow();
        }

        public void Up()
        {
            StopWatching();
            Y--;
            ViewModel.MovementShadowRotate = 0;
            ViewModel.LastMove = ActionDirections.Up;
        }

        public void Down()
        {
            StopWatching();
            Y++;
            ViewModel.MovementShadowRotate = 180;
            ViewModel.LastMove = ActionDirections.Down;
        }

        public void Left()
        {
            StopWatching();
            X--;
            ViewModel.MovementShadowRotate = 270;
            ViewModel.LastMove = ActionDirections.Left;
        }

        public void Right()
        {
            StopWatching();
            X++;
            ViewModel.MovementShadowRotate = 90;
            ViewModel.LastMove = ActionDirections.Right;
        }

        #endregion

        private void InitiateMovementShadow()
        {
            ViewModel.MovementShadowTopDistance = -(4 * _cubeHeigth);
            ViewModel.MovementShadowLeftDistance = -(2 * _cubeWidth);
            ViewModel.CenterX = (double)_cubeWidth / 2;
            ViewModel.CenterY = (double)_cubeHeigth / 2;
            ViewModel.MovementShadowWidth = 11 * _cubeWidth;
            ViewModel.MovementShadowHeight = 9 * _cubeHeigth;
            ViewModel.MovementShadowVisibility = Visibility.Visible;
        }
    }
}
