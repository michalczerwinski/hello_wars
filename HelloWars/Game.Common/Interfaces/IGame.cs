using System.Collections.Generic;
using System.Windows.Controls;

namespace Common.Interfaces
{
    public interface IGame
    {
        List<ICompetitor> Competitors { get; }
        long RoundNumber { get; set; }
        bool PerformNextRound();
        UserControl GetVisualisation();
        IDictionary<ICompetitor, double> GetResults();
        void AddCompetitor(ICompetitor competitor);
        void Start();
        void Reset();
    }
}
