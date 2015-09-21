using System.Windows.Media;
using Common.Models;
using Game.CubeClash.Enums;
using Game.CubeClash.Interfaces;

namespace Game.CubeClash.ViewModels
{
   public  class ExplosionViewModel:BindableBase, IUnmovableObject
    {
        private int _x;
        private int _y;
        private ImageSource _image;
        private int _heigth;
        private int _width;

        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        public int Heigth
        {
            get { return _heigth; }
            set { SetProperty(ref _heigth, value); }
        }

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

       public UnmovableObjectTypes Type
       {
           get
           {
               return UnmovableObjectTypes.Explosion;
           }
       }
    }
}
