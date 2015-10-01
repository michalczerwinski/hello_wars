using System.Windows.Media;
using Common.Utilities;

namespace Game.AntWars.ViewModels.BaseUnits
{
    public class ExplosionViewModel : BindableBase
    {
        private int _x;
        private int _y;
        private ImageSource _image;

        public ImageSource Image
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
