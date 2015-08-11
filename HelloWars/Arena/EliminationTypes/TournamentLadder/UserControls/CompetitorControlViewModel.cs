using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    public class CompetitorControlViewModel:BindableBase
    {
        public Competitor Competitor { get; set; }

        public Point HeadPoint;
    }
}
