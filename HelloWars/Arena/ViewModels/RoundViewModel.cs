using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Models;

namespace Arena.ViewModels
{
    public class RoundViewModel : BindableBase
    {
        private RoundList _roundList;
        public RoundList RoundList
        {
            get { return _roundList; }
            set { SetProperty(ref _roundList, value); }
        }
    }
}
