using System.Collections.Generic;
using System.Windows.Controls;
using Bot = BotClient.BotClient;

namespace Arena.Interfaces
{
    public interface IGame
    {
        UserControl GetVisualisation();
        bool PerformNextMove();
        Dictionary<Bot, double> GetResoult();
        List<Bot> Competitors { get; set; }

        long RoundNumber { get; set; }
    }
}
