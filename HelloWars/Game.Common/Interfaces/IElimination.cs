using System.Collections.Generic;
using System.Windows.Controls;

namespace Common.Interfaces
{
    public interface IElimination
    {
        List<ICompetitor> Bots { get; set; }
        UserControl GetVisualization();
        IList<ICompetitor> GetNextCompetitors();
        void SetLastDuelResult(IDictionary<ICompetitor, double> result);
    }
}
