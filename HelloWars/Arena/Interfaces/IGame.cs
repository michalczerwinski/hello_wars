using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Interfaces
{
    public interface IGame
    {
        long RoundNumber { get; set; }
        bool PerformNextRound();
    }
}
