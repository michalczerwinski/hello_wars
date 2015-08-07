using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Configuration;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGameDescription
    {
        IGame CreateNewGame(IEnumerable<CompetitorUrl> competitorUrls);
        UserControl GetVisualisation();
    }

    public interface IGame
    {
        long RoundNumber { get; set; }
        bool PerformNextRound();
    }
}
