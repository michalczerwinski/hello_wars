using System.Drawing;
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
        private Image _image;
        private AvailableMoves _direction;

        public Image Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public AvailableMoves Direction
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

        public MissileViewModel()
        {
            Color = new SolidColorBrush(Colors.Black);
        }
    }
}
