namespace Arena.Games.Tanks.ViewModels
{
    public class TankGameViewModel : BindableBase
    {
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
    }
}
