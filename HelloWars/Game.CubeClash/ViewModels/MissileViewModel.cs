using System.Windows.Media;
using Common.Models;
using Game.CubeClash.Enums;

namespace Game.CubeClash.ViewModels
{
    public class MissileViewModel : BindableBase
    {
        private int _x;
        private int _range;
        private int _y;
        private SolidColorBrush _color;
        private ImageSource _image;
        private ActionDirections _direction;
        private int _angle;
        private double _centerX;
        private double _centerY;
       
        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public ActionDirections Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }

        public int Range
        {
            get { return _range; }
            set { SetProperty(ref _range, value); }
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

        public int Angle
        {
            get { return _angle; }
            set { SetProperty(ref _angle, value); }
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
    }
}
