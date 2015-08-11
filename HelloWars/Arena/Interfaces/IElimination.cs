using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IElimination
    {
        List<Competitor> Competitors { get; set; }
        UserControl GetVisualization(List<Competitor> competitors);
        IList<Competitor> GetNextCompetitors();
    }
}
