using Game.Common.Interfaces;

namespace Game.Common.Models
{
    public abstract class BotLocalClient<TArenaInfo, TMove> : Competitor, IBotClient<TArenaInfo, TMove>
    {
        protected BotLocalClient(ICompetitor competitor) : base(competitor)
        {
        }

        public abstract TMove NextMove(TArenaInfo arenaInfo);
    }
}
