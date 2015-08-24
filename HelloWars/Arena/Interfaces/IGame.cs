using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Models;

namespace Arena.Interfaces
{
    public interface IGame
    {
        List<Competitor> Competitors { get; set; } 
        long RoundNumber { get; set; }
        bool PerformNextMove();
        EventHandler GameFinishHandler { get; set; }
    }
}
