using System.Collections.ObjectModel;
using System.Windows.Input;
using Common.Models;
using Game.CubeClash.Commands;
using Game.CubeClash.Interfaces;

namespace Game.CubeClash.ViewModels
{
    public class CubeClashViewModel : BindableBase
    {
        public ObservableCollection<IMovableObiects> PlayersCollection { get; set; }
        public ObservableCollection<IUnmovableObjeect> GridCollection { get; set; }

        private int _battlegroundWidth;
        private int _battlegroundHeigth;
        private int _cubeWidth;
        private int _cubeHeigth;
        private int _columnCount;
        private int _rowCount;

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

        public int CubeWidth
        {
            get { return _cubeWidth; }
            set { SetProperty(ref _cubeWidth, value); }
        }

        public int CubeHeigth
        {
            get { return _cubeHeigth; }
            set { SetProperty(ref _cubeHeigth, value); }
        }

        public ICommand DoMoveCommand
        {
            get { return new DoMoveCommand(this); }
        }

        public ICommand WatchCommand
        {
            get { return new WatchCommand(this); }
        }

        public ICommand GoLeftCommand
        {
            get { return new GoLeftCommand(this); }
        }

        public ICommand GoRightCommand
        {
            get { return new GoRightCommand(this); }
        }

        public ICommand GoUpCommand
        {
            get { return new GoUpCommand(this); }
        }

        public ICommand GoDownCommand
        {
            get { return new GoDownCommand(this); }
        }

        public ICommand FireMisleCommand
        {
            get { return new FireMissleCommand(this); }
        }
    }
}

