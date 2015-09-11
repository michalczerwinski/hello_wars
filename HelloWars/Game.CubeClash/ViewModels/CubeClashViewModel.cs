using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Common;
using Common.Models;
using Game.CubeClash.Commands;

namespace Game.CubeClash.ViewModels
{
    public class CubeClashViewModel : BindableBase
    {
        public ObservableCollection<CubeViewModel> PlayersCollection { get; set; }
        public ObservableCollection<GridUnitViewModel> GridCollection { get; set; }
        public ICommand _doMoveCommand;
        private int _battlegroundWidth;
        private int _battlegroundHeigth;

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

        private int _columnCount;
        private int _rowCount;

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


        public ICommand DoMoveCommand
        {
            get { return _doMoveCommand ?? (_doMoveCommand = new DoMoveCommand(this)); }
        }

        public CubeClashViewModel()
        {
            RowCount = 40;
            ColumnCount = 40;

            BattlegroundWidth = RowCount * 10;
            BattlegroundHeigth = ColumnCount * 10;

            var cubeWidth = 10;
            var cubeHeight = 10;

            GridCollection = new ObservableCollection<GridUnitViewModel>();
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                {
                    var ccc = new GridUnitViewModel();
                    ccc.X = i;
                    ccc.Y = j;

                    GridCollection.Add(ccc);
                }

            PlayersCollection = new ObservableCollection<CubeViewModel>();
            for (int i = 0; i < 1; i++)
            {
                var ccc = new CubeViewModel(cubeWidth, cubeHeight);
                ccc.X = 10;
                ccc.Y = 10;
                ccc.Color = new SolidColorBrush(Colors.BlueViolet);
                PlayersCollection.Add(ccc);


                var sss = new CubeViewModel(cubeWidth, cubeHeight);
                sss.X = 10;
                sss.Y = 20;
                sss.Color = new SolidColorBrush(Colors.Aqua);

                PlayersCollection.Add(sss);


                var ddd = new CubeViewModel(cubeWidth, cubeHeight);
                ddd.X = 10;
                ddd.Y = 30;
                ddd.Color = new SolidColorBrush(Colors.Lime);


                PlayersCollection.Add(ddd);

            }
        }
    }
}
