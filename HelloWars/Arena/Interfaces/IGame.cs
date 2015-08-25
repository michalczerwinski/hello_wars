using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGame
    {
        List<Competitor> Competitors { get; set; } 
        long RoundNumber { get; set; }
        bool PerformNextMove();
        UserControl GetVisualisation();
        IGame CreateNewGame(List<Competitor> competitors);
    }
}
