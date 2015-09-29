using System.Collections.Generic;
using Common.Interfaces;
using Elimination.TournamentLadder.UserControls;

namespace Elimination.TournamentLadder.ViewModels
{
    public class TournamentLadderViewModel
    {
        public List<ICompetitor> Bots;
        public List<List<BotUserControl>> StageLists;

        public TournamentLadderViewModel(List<ICompetitor> bots)
        {
            Bots = bots;
        }
    }
}
