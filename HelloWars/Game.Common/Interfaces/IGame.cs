using System.Collections.Generic;
using System.Windows.Controls;
using Common.Models;

namespace Common.Interfaces
{
    public interface IGame
    {
        RoundResult PerformNextRound();
        UserControl GetVisualisationControl();
        void SetupNewGame(IEnumerable<ICompetitor> competitors);
        void Reset();
        void SetPreview(object boardState);
    }
}
