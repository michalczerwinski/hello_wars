using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.EliminationTypes.TournamentLadder.Models;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadder : IElimination
    {
        public List<Competitor> Competitors { get; set; }

        public Queue<Tuple<WrappedCompetitor, WrappedCompetitor>> DuelPairList { get; set; }

        public UserControl GetVisualization()
        {
            if (Competitors != null)
            {
                return new TournamentLadderControl(new TournamentLadderViewModel(Competitors));
            }
            else
            {
                return null;
            }
        }

        //public IList<CompetitorUrl> GetNextCompetitors(List<Competitor> competitors)
        //{
        //    throw new NotImplementedException();
        //}

        public IList<Competitor> GetNextCompetitors()
        {
            var result = new List<Competitor>();

            if (DuelPairList != null)
            {
                result.Add(DuelPairList.First().Item1.Competitor);
                result.Add(DuelPairList.First().Item2.Competitor);
                DuelPairList.Dequeue();
                return result;
            }
            return null;
        }
    }
}
