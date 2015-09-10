using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using Common.Models;

namespace Elimination.RoundRobin.ViewModels
{
    public class RoundRobinViewModel : BindableBase
    {
        private int _numberOfRepeat;
        private bool _clearScoreBoard;
        public List<BotViewModel> Bots { get; set; }
        public List<ICompetitor> Competitors { get; set; }

        public RoundRobinViewModel(List<ICompetitor> competitors)
        {
            Competitors = competitors;
            Bots = WrapBots(competitors);
            NumberOfRepeat = 1;
        }

        public int NumberOfRepeat
        {
            get { return _numberOfRepeat; }
            set { SetProperty(ref _numberOfRepeat, value); }
        }

        public bool ClearScoreBoard
        {
            get { return _clearScoreBoard; }
            set { SetProperty(ref _clearScoreBoard, value); }
        }

        private List<BotViewModel> WrapBots(IEnumerable<ICompetitor> bots)
        {
            return bots.Select(bot => new BotViewModel(bot)).ToList();
        }
    }
}
