using System.Collections.Generic;
using Arena.Eliminations.TournamentLadder.UserControls;
using Bot = BotClient.BotClient;

namespace Arena.Eliminations.TournamentLadder.ViewModels
{
    public class TournamentLadderViewModel
    {
        public List<Bot> Bots;
        public List<List<BotUserControl>> StageLists;

        public TournamentLadderViewModel(List<Bot> bots)
        {
            Bots = bots;
        }
    }
}
