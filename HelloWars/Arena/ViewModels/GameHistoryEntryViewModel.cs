using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Arena.ViewModels
{
    public class GameHistoryEntryViewModel
    {
        public string GameDescription { get; set; }
        public List<RoundPartialHistory> History { get; set; }
    }
}
