using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand DoMoveCommand
        {
            get { return _doMoveCommand ?? (_doMoveCommand = new DoMoveCommand(this)); }
        }

        public CubeClashViewModel()
        {
            BattlegroundWidth = 100;
            BattlegroundHeigth = 100;

            GridCollection = new ObservableCollection<GridUnitViewModel>();
            for (int i = 0; i < BattlegroundWidth; i++)
                for (int j = 0; j < BattlegroundHeigth; j++)
                {
                    var ccc = new GridUnitViewModel();
                    ccc.X = i;
                    ccc.Y = j;
                   
                    GridCollection.Add(ccc);
                }

            PlayersCollection = new ObservableCollection<CubeViewModel>();
            for (int i = 0; i < 1; i++)
            {
                var ccc = new CubeViewModel();
                ccc.X = 1;
                ccc.Y = 1;
               
                PlayersCollection.Add(ccc);
            }
        }
    }
}
