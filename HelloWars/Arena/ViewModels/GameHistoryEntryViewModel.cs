using System.Collections.Generic;
using Common.Models;

namespace Arena.ViewModels
{
    public class GameHistoryEntryViewModel
    {
        public string GameDescription { get; set; }
        public List<RoundPartialHistory> History { get; set; }
    }
}
