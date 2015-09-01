namespace Arena.Utilities
{
    public class GameScore
    {
        public double Score;
        public BotClient.BotClient Oponent;

        public GameScore(BotClient.BotClient oponent, double score)
        {
            Score = score;
            Oponent = oponent;
        }
    }
}
