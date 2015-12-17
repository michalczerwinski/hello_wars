using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Arena.Commands;
using Common.Interfaces;
using Common.Utilities;

namespace Arena.ViewModels
{
    public class WinnerSelectionViewModel : BindableBase
    {
        private ICompetitor _selectedWinner;
        private IEnumerable<ICompetitor> _competitors;

        public ICompetitor SelectedWinner
        {
            get { return _selectedWinner;}
            set { SetProperty(ref _selectedWinner, value); }

        }
        public IEnumerable<ICompetitor> Competitors 
        {
            get { return _competitors; }
            set { SetProperty(ref _competitors, value); }
        }

        public WinnerSelectionViewModel(IEnumerable<ICompetitor> competitors )
        {
            Competitors = competitors;
            SelectedWinner = Competitors.First();
        }
    }
}
