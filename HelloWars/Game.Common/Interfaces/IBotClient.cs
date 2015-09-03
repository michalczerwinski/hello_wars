namespace Common.Interfaces
{
    public interface IBotClient<in TArenaInfo, out TMove> : ICompetitor
    {
        TMove NextMove(TArenaInfo arenaInfo);
    }
}
