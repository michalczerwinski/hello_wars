using System;
using System.Collections.Generic;
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
