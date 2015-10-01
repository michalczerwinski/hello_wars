using System.Windows;
using System.Windows.Media;
using Common.Helpers;
using Common.Interfaces;
using Common.Utilities;
using Game.AntWars.Enums;
using Game.AntWars.Interfaces;
using Game.AntWars.Properties;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Models
{
    public class AntModel : BotClientBase<SurroundingAreaInfo, BotMove>, IMovableObject
    {
        private static ImageSource _yellowMissileImage = ResourceImageHelper.LoadImage(Resources.yellowMissile);
        private static ImageSource _redMissileImage = ResourceImageHelper.LoadImage(Resources.redMissile2);

        public ICompetitor Competitor { get; set; }
        public AntViewModel ViewModel { get; set; }
        private readonly int _antWidth;
        private readonly int _antHeigth;

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

        public AntModel(AntViewModel viewModel, ICompetitor competitor)
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

            if (ViewModel.Image == AntViewModel.RedAntImage)
            {
                missile.ViewModel.Image = _redMissileImage;
            }
            else if (ViewModel.Image == AntViewModel.YellowAntImage)
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
            ViewModel.MovementShadowTopDistance = -(4 * _antHeigth);
            ViewModel.MovementShadowLeftDistance = -(2 * _antWidth);
            ViewModel.CenterX = (double)_antWidth / 2;
            ViewModel.CenterY = (double)_antHeigth / 2;
            ViewModel.MovementShadowWidth = 11 * _antWidth;
            ViewModel.MovementShadowHeight = 9 * _antHeigth;
            ViewModel.MovementShadowVisibility = Visibility.Visible;
        }

        public MovableObjectsTypes Type
        {
            get { return MovableObjectsTypes.Bot; }
        }
    }
}
