using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.Games.PickTheWinner
{
    public class PickTheWInnerGame : IGame
    {
        public List<Competitor> Competitors { get; set; }

        public long RoundNumber { get; set; }

        public bool PerformNextMove()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            var looser = rand.Next(0, 1);

            Competitors[looser].StilInGame = false;

            return true;
        }

        public EventHandler GameFinishHandler { get; set; }
    }
}
