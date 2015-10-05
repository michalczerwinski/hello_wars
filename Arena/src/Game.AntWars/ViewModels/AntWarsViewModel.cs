using System.Collections.ObjectModel;
using System.Windows;
using Common.Utilities;
using Game.AntWars.Interfaces;

namespace Game.AntWars.ViewModels
{
    public class AntWarsViewModel : BindableBase
    {
        public ObservableCollection<IMovableObject> MovableObjectsCollection { get; set; }
        public ObservableCollection<IUnmovableObject> BattlefieldObjectsCollection { get; set; }

        private int _battlegroundWidth;
        private int _battlegroundHeigth;
        private int _antWidth;
        private int _antHeigth;
        private int _columnCount;
        private int _rowCount;
        private Visibility _splashScreenVisible;

        public int BattlegroundHeigth
        {
            get { return _battlegroundWidth; }
            set { SetProperty(ref _battlegroundWidth, value); }
        }

        public int BattlegroundWidth
        {
            get { return _battlegroundHeigth; }
            set { SetProperty(ref _battlegroundHeigth, value); }
        }

        public int ColumnCount
        {
            get { return _columnCount; }
            set { SetProperty(ref _columnCount, value); }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set { SetProperty(ref _rowCount, value); }
        }

        public int AntWidth
        {
            get { return _antWidth; }
            set { SetProperty(ref _antWidth, value); }
        }

        public int AntHeigth
        {
            get { return _antHeigth; }
            set { SetProperty(ref _antHeigth, value); }
        }

        public Visibility SplashScreenVisible
        {
            get { return _splashScreenVisible; }
            set { SetProperty(ref _splashScreenVisible, value); }
        }

        public AntWarsViewModel()
        {
            MovableObjectsCollection = new ObservableCollection<IMovableObject>();
            BattlefieldObjectsCollection = new ObservableCollection<IUnmovableObject>();
            SplashScreenVisible = Visibility.Visible;
        }
    }
}