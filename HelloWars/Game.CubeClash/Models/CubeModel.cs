using System.Windows;
using System.Windows.Media;
using Common.Interfaces;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Models
{
    public class CubeModel : BotClientBase<SurroundingAreaInfo, CubeMove>, IMovableObiects
    {
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

        public CubeModel(CubeViewModel viewModel, ICompetitor competitor, int width, int heigth)
            : base(competitor)
        {
            Competitor = competitor;
            _cubeWidth = width;
            _cubeHeigth = heigth;
            ViewModel = viewModel;
            InitiateMovementShadow();
        }

        public void Attack()
        {
            StopWatching();
            ViewModel.IsAttacking = "True";
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
            ViewModel.MovementShadowRotate = 270;
            ViewModel.LastMove = AvailableMoves.Up;
        }

        public void Down()
        {
            StopWatching();
            Y++;
            ViewModel.MovementShadowRotate = 90;
            ViewModel.LastMove = AvailableMoves.Down;
        }

        public void Left()
        {
            StopWatching();
            X--;
            ViewModel.MovementShadowRotate = 180;
            ViewModel.LastMove = AvailableMoves.Left;
        }

        public void Right()
        {
            StopWatching();
            X++;
            ViewModel.MovementShadowRotate = 0;
            ViewModel.LastMove = AvailableMoves.Right;
        }

        public MissleModel FireMissle()
        {
            var missle = new MissleModel(new MissleViewModel())
            {
                X = 10,
                Y = 10,
                Direction = ViewModel.LastMove,
                Range = 10
            };
            return missle;
        }

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
