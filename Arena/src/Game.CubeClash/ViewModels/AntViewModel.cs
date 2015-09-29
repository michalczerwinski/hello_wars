using System.Windows;
using System.Windows.Media;
using Common.Helpers;
using Common.Models;
using Game.AntWars.Enums;
using Game.AntWars.Properties;

namespace Game.AntWars.ViewModels
{
    public class AntViewModel : BindableBase
    {
        public static ImageSource RedAntImage = ResourceImageHelper.LoadImage(Resources.redAnt);
        public static bool IsRedAntAdded;
        public static ImageSource YellowAntImage = ResourceImageHelper.LoadImage(Resources.yellowAnt);
        public static bool IsYellowAntAdded;

        private int _x;
        private int _y;
        private double _movementShadowTopDistance;
        private int _movementShadowRotate;
        private double _movementShadowLeftDistance;
        private double _movementShadowWidth;
        private double _movementShadowHeight;
        private double _centerX;
        private double _centerY;
        private string _isAttacking;
        private bool _isAttackingAnimationCompleated;
        private SolidColorBrush _color;
        public ActionDirections LastMove;
        private Visibility _movementShadowVisibility;
        private ImageSource _image;

        public SolidColorBrush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
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

        public Visibility MovementShadowVisibility
        {
            get { return _movementShadowVisibility; }
            set { SetProperty(ref _movementShadowVisibility, value); }
        }

        public string IsAttacking
        {
            get { return _isAttacking; }
            set { SetProperty(ref _isAttacking, value); }
        }

        public bool IsAttackingAnimationCompleated
        {
            get { return _isAttackingAnimationCompleated; }
            set
            {
                //_isAttackingAnimationCompleated = value;
                //switch (LastMove)
                //{
                //    case ActionDirections.Up:
                //        {
                //            Y -= 4;
                //            break;
                //        }
                //    case ActionDirections.Left:
                //        {
                //            X -= 4;
                //            break;
                //        }
                //    case ActionDirections.Down:
                //        {
                //            Y += 4;
                //            break;
                //        }
                //    case ActionDirections.Right:
                //        {
                //            X += 4;
                //            break;
                //        }
                //}

                IsAttacking = "False";
                _isAttackingAnimationCompleated = false;
            }
        }

        public AntViewModel()
        {
            if (!IsRedAntAdded)
            {
                IsRedAntAdded = true;
                Image = RedAntImage;
            }
            else if (!IsYellowAntAdded)
            {
                IsYellowAntAdded = true;
                Image = YellowAntImage;
            }
        }
    }
}
