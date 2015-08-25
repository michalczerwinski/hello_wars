using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGame
    {
        UserControl GetVisualisation();
        List<Competitor> Competitors { get; set; }
        bool PerformNextMove();

        long RoundNumber { get; set; }
        IGame CreateNewGame(List<Competitor> competitors);
    }
}
