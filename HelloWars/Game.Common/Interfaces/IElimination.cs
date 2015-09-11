using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Common.Models;

namespace Common.Interfaces
{
    [InheritedExport(typeof(IElimination))]
    public interface IElimination
    {
        List<ICompetitor> Bots { get; set; }
        UserControl GetVisualization(IConfigurable configuration);
        IList<ICompetitor> GetNextCompetitors();
        void SetLastDuelResult(IDictionary<ICompetitor, double> result);
        string GetGameDescription();
    }
}
