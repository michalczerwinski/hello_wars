using Common.Helpers;
using Common.Interfaces;

namespace Common.Models
{
    public class BotClientBase<TArenaInfo, TMove> : Competitor, IBotClient<TArenaInfo, TMove>
    {
        public BotClientBase()
        {}

        public BotClientBase(ICompetitor competitor) : base(competitor)
        {}

        public virtual TMove NextMove(TArenaInfo arenaInfo)
        {
            return WebClientHelper.PostData<TArenaInfo, TMove>(Url + Resources.PerformNextMoveUrlSuffix, arenaInfo);
        }
    }
}
