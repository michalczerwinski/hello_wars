using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.Games.Tanks
{
    public class Tanks : IGameDescription
    {
        public long RoundNumber { get; set; }

        public bool PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public System.Windows.Controls.UserControl GetVisualisation()
        {
            throw new NotImplementedException();
        }

        public IGame CreateNewGame(IEnumerable<CompetitorUrl> competitorUrls)
        {
            throw new NotImplementedException();
        }
    }
}
