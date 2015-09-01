using System.Collections.Generic;
using System.Windows.Controls;
using Bot = BotClient.BotClient;

namespace Arena.Interfaces
{
    public interface IElimination
    {
        List<Bot> Bots { get; set; }
        UserControl GetVisualization();
        IList<Bot> GetNextCompetitors();
        void SetLastDuelResult(IDictionary<Bot, double> result);
    }
}
