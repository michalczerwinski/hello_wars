using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Common.Interfaces
{
    [InheritedExport(typeof(IGame))]
    public interface IGame
    {
        List<ICompetitor> Competitors { get; }
        long RoundNumber { get; set; }
        string PerformNextRound();
        UserControl GetVisualisation();
        IDictionary<ICompetitor, double> GetResults();
        void AddCompetitor(ICompetitor competitor);
        void Start();
        void Reset();
        bool IsGameFinished();

    }
}
