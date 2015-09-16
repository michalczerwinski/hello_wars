using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Game.CubeClash.Models;

namespace Game.CubeClash.ViewModels
{
    public class GridUnitViewModel : BindableBase
    {
        private int _x;
        private int _y;
        private LandTypes _landType;

        public LandTypes LandType
        {
            get { return _landType; }
            set { SetProperty(ref _landType, value); }
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
