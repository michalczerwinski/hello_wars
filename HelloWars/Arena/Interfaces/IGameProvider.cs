using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGameProvider
    {
        UserControl GetVisualisation();
        IGame CreateNewGame(IEnumerable<Competitor> competitors);
    }
}
