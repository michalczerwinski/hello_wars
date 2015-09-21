using System.Drawing;
using Common.Models;
using Game.CubeClash.Enums;

namespace Game.CubeClash.ViewModels
{
    public class GridUnitViewModel : BindableBase
    {
        private int _x;
        private int _y;
        private MovableObjectsTypes _landType;
        private Image _image;

        public MovableObjectsTypes LandType
        {
            get { return _landType; }
            set { SetProperty(ref _landType, value); }
        }

        public Image Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
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
    }
}
