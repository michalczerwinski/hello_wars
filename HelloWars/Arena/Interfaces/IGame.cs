using System.Collections.Generic;
using System.Windows.Controls;
using Bot = BotClient.BotClient;

namespace Arena.Interfaces
{
    public interface IGame
    {
        List<Bot> Competitors { get; set; }
        long RoundNumber { get; set; }
        bool PerformNextRound();
        UserControl GetVisualisation();
        IDictionary<Bot, double> GetResoult();
    }
}
