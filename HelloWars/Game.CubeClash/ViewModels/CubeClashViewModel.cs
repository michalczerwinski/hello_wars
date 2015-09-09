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
        public ObservableCollection<CubeViewModel> Collection { get; set; }
        public ICommand _doMoveCommand;
        public ICommand DoMoveCommand
        {
            get { return _doMoveCommand ?? (_doMoveCommand = new DoMoveCommand(this)); }
        }

        public CubeClashViewModel()
        {
            Collection = new ObservableCollection<CubeViewModel>();
            for (int i = 0; i < 3; i++)
            {
                var ccc = new CubeViewModel();
                ccc.X = 4 + i;
                ccc.Y = 4 + i;
                Collection.Add(ccc);
            }
        }
    }
}
