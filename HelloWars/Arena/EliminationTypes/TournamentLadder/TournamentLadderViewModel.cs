using System.Collections.Generic;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Bot = BotClient.BotClient;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadderViewModel
    {
        public List<Bot> Competitors;
        public List<List<CompetitorViewControl>> StageLists;

        public TournamentLadderViewModel(List<Bot> competitors)
        {
            Competitors = competitors;
        }
    }
}
