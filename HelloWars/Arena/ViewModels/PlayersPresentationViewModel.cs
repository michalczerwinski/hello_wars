using System.Collections.Generic;
using Common.Interfaces;
using Common.Models;

namespace Arena.ViewModels
{
    public class PlayersPresentationViewModel : BindableBase
    {
        private List<ICompetitor> _listOfCompetitors;

        public List<ICompetitor> ListOfCompetitors
        {
            get { return _listOfCompetitors; }
            set { SetProperty(ref _listOfCompetitors, value); }
        }
    }
}
