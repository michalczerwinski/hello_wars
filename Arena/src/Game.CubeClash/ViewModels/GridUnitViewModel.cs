using System.Windows.Media;
using Common.Helpers;
using Common.Utilities;
using Game.AntWars.Enums;
using Game.AntWars.Properties;

namespace Game.AntWars.ViewModels
{
    public class GridUnitViewModel : BindableBase
    {
        private static ImageSource _lawnImage = ResourceImageHelper.LoadImage(Resources.lawn);
        private static ImageSource _rockImage = ResourceImageHelper.LoadImage(Resources.rock);
        private static ImageSource _woodImage = ResourceImageHelper.LoadImage(Resources.wood);
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
                    case UnmovableObjectTypes.Lawn:
                        {
                            Image = _lawnImage;
                            break;
                        }
                    case UnmovableObjectTypes.Rock:
                        {
                            Image = _rockImage;
                            break;
                        }
                    case UnmovableObjectTypes.Wood:
                        {
                            Image = _woodImage;
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
