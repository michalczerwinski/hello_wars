using System.Collections.Generic;
using Arena.Eliminations.TournamentLadder.UserControls;
using Common.Interfaces;

namespace Arena.Eliminations.TournamentLadder.ViewModels
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
