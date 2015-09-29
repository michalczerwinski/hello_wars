using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;

namespace Arena.ViewModels
{
    public class GameHistoryEntryViewModel
    {
        public string GameDescription { get; set; }
        public ObservableCollection<RoundPartialHistory> History { get; set; }
    }
}
