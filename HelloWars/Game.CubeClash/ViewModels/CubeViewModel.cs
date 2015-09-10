using System.Windows;
using Common.Models;

namespace Game.CubeClash.ViewModels
{
    public class CubeViewModel : BindableBase
    {
        private Point _position;
        private int _x;
        private int _y;
        private int _xSpan;
        private int _ySpan;

        public Point Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public MoveDirection MoveDirection { get; set; }

        public int X
        {
            get { return _x; }
            set
            {
                var csc = value - (XSpan / 2);
                SetProperty(ref _x, csc);
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                var sss = value - (YSpan / 2);
                SetProperty(ref _y, sss);
            }
        }

        public int YSpan
        {
            get { return _ySpan; }
            set { SetProperty(ref _ySpan, value); }
        }

        public int XSpan
        {
            get { return _xSpan; }
            set { SetProperty(ref _xSpan, value); }
        }

        public CubeViewModel()
        {
        }
    }
}
