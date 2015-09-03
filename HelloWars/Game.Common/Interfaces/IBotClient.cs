using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common.Interfaces
{
    public interface IBotClient<in TArenaInfo, out TMove> : ICompetitor
    {
        TMove NextMove(TArenaInfo arenaInfo);
    }
}
