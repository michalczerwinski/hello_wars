using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Common.Models;
using Game.CubeClash.Models;

namespace Game.CubeClash.ViewModels
{
    public class CubeViewModel : BindableBase
    {
        private int _x;
        private int _y;
        private int _xSpan;
        private int _ySpan;
        private SolidColorBrush _color;
        private AvailableMoves _lastMove;
        private double _movementShadowTopDistance;
        private int _movementShadowRotate;
        private double _movementShadowLeftDistance;
        private double _movementShadowWidth;
        private double _movementShadowHeight;
        private double _centerX;
        private double _centerY;
        private string _isAttacking;
        private string _isWatching;
        private bool _isAttackingAnimationCompleated;
        private readonly int _cubeWidth;
        private readonly int _cubeHeigth;


        public SolidColorBrush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public double MovementShadowTopDistance
        {
            get { return _movementShadowTopDistance; }
            set { SetProperty(ref _movementShadowTopDistance, value); }
        }

        public int MovementShadowRotate
        {
            get { return _movementShadowRotate; }
            set { SetProperty(ref _movementShadowRotate, value); }
        }

        public double MovementShadowLeftDistance
        {
            get { return _movementShadowLeftDistance; }
            set { SetProperty(ref _movementShadowLeftDistance, value); }
        }

        public double CenterX
        {
            get { return _centerX; }
            set { SetProperty(ref _centerX, value); }
        }

        public double CenterY
        {
            get { return _centerY; }
            set { SetProperty(ref _centerY, value); }
        }

        public double MovementShadowWidth
        {
            get { return _movementShadowWidth; }
            set { SetProperty(ref _movementShadowWidth, value); }
        }

        public double MovementShadowHeight
        {
            get { return _movementShadowHeight; }
            set { SetProperty(ref _movementShadowHeight, value); }
        }

        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        public int Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }

        private Visibility _movementShadowVisibility;
        public Visibility MovementShadowVisibility
        {
            get { return _movementShadowVisibility; }
            set { SetProperty(ref _movementShadowVisibility, value); }
        }

        public CubeViewModel(int width, int heigth)
        {
            _cubeHeigth = heigth;
            _cubeWidth = width;

            InitiateMovementShadow();
        }

        private void InitiateMovementShadow()
        {
            MovementShadowTopDistance = -(4 * _cubeHeigth);
            MovementShadowLeftDistance = -(2 * _cubeWidth);
            CenterX = (double)_cubeWidth / 2;
            CenterY = (double)_cubeHeigth / 2;
            MovementShadowRotate = 0;
            MovementShadowWidth = 11 * _cubeWidth;
            MovementShadowHeight = 9 * _cubeHeigth;
            MovementShadowVisibility = Visibility.Visible;
        }

        public void Attack()
        {
            StopWatching();
            IsAttacking = "True";
        }

        public void Watch()
        {
            MovementShadowLeftDistance = -100;
            MovementShadowTopDistance = -100;
            MovementShadowHeight = 200;
            MovementShadowWidth = 200;
            IsWatching = "True";
        }

        public void StopWatching()
        {
            InitiateMovementShadow();
            IsWatching = "False";
        }

        public void Up()
        {
            StopWatching();
            Y--;
            MovementShadowRotate = 270;
            _lastMove = AvailableMoves.Up;
        }

        public void Down()
        {
            StopWatching();
            Y++;
            MovementShadowRotate = 90;
            _lastMove = AvailableMoves.Down;
        }

        public void Left()
        {
            StopWatching();
            X--;
            MovementShadowRotate = 180;
            _lastMove = AvailableMoves.Left;
        }

        public void Right()
        {
            StopWatching();
            X++;
            MovementShadowRotate = 0;
            _lastMove = AvailableMoves.Right;
        }

        public string IsAttacking
        {
            get { return _isAttacking; }
            set { SetProperty(ref _isAttacking, value); }
        }

        public string IsWatching
        {
            get { return _isWatching; }
            set
            {
                SetProperty(ref _isWatching, value);


            }
        }

        public bool IsAttackingAnimationCompleated
        {
            get { return _isAttackingAnimationCompleated; }
            set
            {
                _isAttackingAnimationCompleated = value;
                switch (_lastMove)
                {
                    case AvailableMoves.Up:
                        {
                            Y -= 4;
                            break;
                        }
                    case AvailableMoves.Left:
                        {
                            X -= 4;
                            break;
                        }
                    case AvailableMoves.Down:
                        {
                            Y += 4;
                            break;
                        }
                    case AvailableMoves.Right:
                        {
                            X += 4;
                            break;
                        }
                }

                IsAttacking = "False";
                _isAttackingAnimationCompleated = false;
            }
        }
    }
}
