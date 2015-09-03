using System.Collections.Generic;
using System.Windows.Controls;
using Game.Common.Interfaces;

namespace Arena.Interfaces
{
    public interface IElimination
    {
        List<ICompetitor> Bots { get; set; }
        UserControl GetVisualization();
        IList<ICompetitor> GetNextCompetitors();
        void SetLastDuelResult(IDictionary<ICompetitor, double> result);
    }
}
