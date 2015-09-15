using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
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

        public MoveDirection MoveDirection { get; set; }

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

        private double _movementShadowTopDistance;
        private int _movementShadowRotate;
        private double _movementShadowLeftDistance;
        private double _movementShadowWidth;
        private double _movementShadowHeight;
        private double _centerX;
        private double _centerY;
        private string _isGlowing;

        public delegate void AnimationCompleatedDelegate();

        public AnimationCompleatedDelegate AnimationCompleatedHandler { get; set; }

        public string _animationCompleated;
        public string AnimationCompleated
        {
            get { return _animationCompleated; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    
                if (value == "") { }

                }
            }
        }


        public string IsGlowing
        {
            get { return _isGlowing; }
            set { SetProperty(ref _isGlowing, value); }
        }

        public CubeViewModel(int width, int heigth)
        {
            MovementShadowTopDistance = -(4 * heigth);
            MovementShadowLeftDistance = -(2 * width);
            CenterX = (double)width / 2;
            CenterY = (double)heigth / 2;
            MovementShadowRotate = 0;
            MovementShadowWidth = 11 * width;
            MovementShadowHeight = 9 * heigth;
            MovementShadowVisibility = Visibility.Visible;
        }

        public void Attack()
        {
            IsAttacking = "True";

            X += 7;
        }

        public void Rotate()
        {

        }

        public void Up()
        {

        }

        public void Down()
        {

        }
        public void Left()
        {

        }
        public void Right()
        {

        }

        private string _isAttacking;
        public string IsAttacking
        {
            get { return _isAttacking; }
            set { SetProperty(ref _isAttacking, value); }
        }
    }
}
