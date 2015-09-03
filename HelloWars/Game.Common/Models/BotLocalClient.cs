using Common.Interfaces;

namespace Common.Models
{
    public abstract class BotLocalClient<TArenaInfo, TMove> : BotClientBase<TArenaInfo, TMove>
    {
        protected BotLocalClient(ICompetitor competitor) : base(competitor)
        {}

        public abstract override TMove NextMove(TArenaInfo arenaInfo);
    }
}
