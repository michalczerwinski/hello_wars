using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Windows.Controls;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.Games.Tanks
{
    public class Tanks : IGame
    {
        public long RoundNumber { get; set; }

        public bool PerformNextMove()
        {
            throw new NotImplementedException();
        }

        public EventHandler GameFinishHandler
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Competitor> Competitors { get; set; }
    }
}
