using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Utilities;

namespace Game.DynaBlaster.Models
{
    public class DynaBlasterControlViewModel : BindableBase
    {
        public BindableArray<bool> Board;

        public DynaBlasterControlViewModel()
        {
            Board = new BindableArray<bool>(15, 15);
        }
    }
}
