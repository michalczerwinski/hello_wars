using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Common.Interfaces;
using Common.Utilities;
using Elimination.RoundRobin.Commands;

namespace Elimination.RoundRobin.ViewModels
{
    public class RoundRobinViewModel : BindableBase
    {
        private int _numberOfRepeat;
        private bool _clearScoreBoard;
        private List<BotViewModel> _bots;
        public List<ICompetitor> Competitors { get; set; }
        private ICommand _resetCommand;

        public ICommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new ResetCommand(this));
            }
        }

        public List<BotViewModel> Bots
        {
            get { return _bots; }
            set { SetProperty(ref _bots, value); }
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

        public RoundRobinViewModel(List<ICompetitor> competitors)
        {
            Competitors = competitors;
            Bots = WrapBots(competitors);
            NumberOfRepeat = 1;
        }

        private List<BotViewModel> WrapBots(IEnumerable<ICompetitor> bots)
        {
            return bots.Select(bot => new BotViewModel(bot)).ToList();
        }
    }
}
