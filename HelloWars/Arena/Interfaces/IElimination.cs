using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Configuration;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IElimination
    {
        List<string> Competitors { get; set; }
        UserControl GetVisualization(List<Competitor> competitors);
        IList<CompetitorUrl> GetNextCompetitors(CompetitorUrl lastWinner);
    }
}
