using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Common.Interfaces
{
    [InheritedExport(typeof(IElimination))]
    public interface IElimination
    {
        List<ICompetitor> Bots { get; set; }
        UserControl GetVisualization();
        IList<ICompetitor> GetNextCompetitors();
        void SetLastDuelResult(IDictionary<ICompetitor, double> result);
    }
}
