using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    public class TournamentLadder :  IElimination
    {
        public List<Competitor> Competitors { get; set; }
        public Queue<Tuple<Competitor, Competitor>> DuelPairList { get; set; }

        public UserControl GetVisualization()
        {
            if (Competitors != null)
            {
                var startingNumberOfCompetitors = CalculateCompetitorCount(Competitors);
                
                AddBotsToCompetitorsList(startingNumberOfCompetitors - Competitors.Count);
              //  CreateDuelsList();

                return new TournamentLadderControl(new TournamentLadderViewModel(Competitors));
            }
            else
            {
                return null;
            }
        }

        private void CreateDuelsList()
        {
            DuelPairList = new Queue<Tuple<Competitor, Competitor>>();

            for (int i = 0; i < Competitors.Count; i++)
            {
                var pair = new Tuple<Competitor, Competitor>(Competitors[i], Competitors[++i]);

                DuelPairList.Enqueue(pair);
            }
        }


        public IList<Competitor> GetNextCompetitors()
        {
            var result = new List<Competitor>();

            result.Add(Competitors[0]);
            result.Add(Competitors[1]);
            //if (DuelPairList.Count != 0)
            //{
            //    result.Add(DuelPairList.First().Item1);
            //    result.Add(DuelPairList.First().Item2);
            //    DuelPairList.Dequeue();
            //    return result;
            //}
            return result;
        }

        private void AddBotsToCompetitorsList(int botCount)
        {
            for (int i = 0; i < botCount; i++)
            {
                var competitor = new Competitor();
                Competitors.Add(competitor);
            }
        }

        private static int CalculateCompetitorCount(List<Competitor> competitors)
        {
            var competitorCount = competitors.Count;
            if (competitorCount < 3) competitorCount = 2;
            if (competitorCount > 2 && competitorCount < 5)
            {
                competitorCount = 4;
            }
            if (competitorCount > 4 && competitorCount < 9)
            {
                competitorCount = 8;
            }
            if (competitorCount > 8 && competitorCount < 17)
            {
                competitorCount = 16;
            }
            if (competitorCount > 16 && competitorCount < 33)
            {
                competitorCount = 32;
            }
            if (competitorCount > 32 && competitorCount < 65)
            {
                competitorCount = 32;
            }
            return competitorCount;
        }

    }
}
