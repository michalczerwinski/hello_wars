using System.Collections.Generic;
using Common.Interfaces;
using Common.Models;

namespace Elimination.RoundRobin.ViewModels
{
    public class RoundRobinViewModel : BindableBase
    {
        public List<BotViewModel> Bots { get; set; }
        public List<ICompetitor> Competitors { get; set; }

        public RoundRobinViewModel(List<ICompetitor> competitors)
        {
            Competitors = competitors;
            Bots = WrapBots(competitors);
        }

        private List<BotViewModel> WrapBots(List<ICompetitor> bots)
        {
            var result = new List<BotViewModel>();
            foreach (var bot in bots)
            {
                var newBot = new BotViewModel(bot);
                result.Add(newBot);
            }

            return result;
        }
    }
}
