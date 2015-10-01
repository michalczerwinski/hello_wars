using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Models
{
    public class RoundResult
    {
        public bool IsFinished { get; set; }
        public Dictionary<ICompetitor, double> FinalResult { get; set; }
        public List<RoundPartialHistory> History { get; set; }
        public string OutputText { get; set; }
    }
}
