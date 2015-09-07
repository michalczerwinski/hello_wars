using System.Collections.Generic;
using Common.Interfaces;
using Eliminations.TournamentLadder.UserControls;

namespace Eliminations.TournamentLadder.ViewModels
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
