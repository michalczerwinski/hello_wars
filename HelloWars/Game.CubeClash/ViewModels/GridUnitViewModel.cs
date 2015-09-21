using System.Windows.Media;
using Common.Helpers;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Properties;

namespace Game.CubeClash.ViewModels
{
    public class GridUnitViewModel : BindableBase
    {
        private int _x;
        private int _y;
        private UnmovableObjectTypes _landType;
        private ImageSource _image;

        public ImageSource Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public UnmovableObjectTypes LandType
        {
            get { return _landType; }
            set
            {
                _landType = value;
                switch (value)
                {
                    case UnmovableObjectTypes.None:
                        {
                            Image = ResourceImageHelper.LoadImage(Resources.grass);
                            break;
                        }
                    case UnmovableObjectTypes.SolidWall:
                        {
                            Image = ResourceImageHelper.LoadImage(Resources.HardBlock);
                            break;
                        }
                    case UnmovableObjectTypes.Hole:
                        {
                            Image = ResourceImageHelper.LoadImage(Resources.bomb);
                            break;
                        }
                }
            }
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
