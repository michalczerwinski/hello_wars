using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    public interface IBotClient<TArenaInfo, TMove> : ICompetitor
    {
        Task<TMove> NextMoveAsync(TArenaInfo arenaInfo);
    }
}
