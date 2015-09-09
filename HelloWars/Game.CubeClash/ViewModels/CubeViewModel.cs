using System.Windows;
using Common.Models;

namespace Game.CubeClash.ViewModels
{
    public class CubeViewModel : BindableBase
    {
        private Point _position;
        private int _x;
        private int _y;

        public Point Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x,value); }
        }

        public int Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }
    }
}
