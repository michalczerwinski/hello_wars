using System;
using System.Collections.Generic;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.Games.PickTheWinner
{
    public class PickTheWInnerGame : IGame
    {
        public List<Competitor> Competitors { get; set; }

        public long RoundNumber { get; set; }

        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public bool PerformNextMove()
        {
            var looser = _rand.Next(0, 2);

            Competitors[looser].StilInGame = false;

            return true;
        }

        public EventHandler GameFinishHandler { get; set; }
    }
}
