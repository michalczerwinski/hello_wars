using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGameDescription
    {
        IGame CreateNewGame(IEnumerable<CompetitorUrl> competitorUrls);
        UserControl GetVisualisation();
    }
}
