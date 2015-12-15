using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Models;

namespace Common.Interfaces
{
    [InheritedExport(typeof(IGame))]
    public interface IGame
    {
        Task<RoundResult> PerformNextRoundAsync();
        UserControl GetVisualisationUserControl(IConfigurable configuration);
        void SetupNewGame(IEnumerable<ICompetitor> competitors);
        void Reset();
        void SetPreview(object boardState);
        string GetGameRules();
        void ApplyConfiguration(string configurationXml);
        void ChangeDelayTime(int delayTime);
        IEnumerable<ICompetitor> GetCurrentCompetitors();
    }
}
