using System.Collections.Generic;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Bot = BotClient.BotClient;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel
    {
        public List<Bot> Bots;
        public List<List<BotViewControl>> StageLists;

        public TournamentLadderViewModel(List<Bot> bots)
        {
            Bots = bots;
        }
    }
}
