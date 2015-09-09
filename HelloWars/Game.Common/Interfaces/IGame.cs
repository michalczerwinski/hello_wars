using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Common.Models;

namespace Common.Interfaces
{
    [InheritedExport(typeof(IGame))]
    public interface IGame
    {
        RoundResult PerformNextRound();
        UserControl GetVisualisationUserControl(IConfigurable configuration);
        void SetupNewGame(IEnumerable<ICompetitor> competitors);
        void Reset();
        void SetPreview(object boardState);
    }
}
