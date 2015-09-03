using Game.Common.Interfaces;

namespace Arena.Utilities
{
    public class GameScore
    {
        public double Score;
        public ICompetitor Oponent;

        public GameScore(ICompetitor oponent, double score)
        {
            Score = score;
            Oponent = oponent;
        }
    }
}
