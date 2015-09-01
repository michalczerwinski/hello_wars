using System.Collections.Generic;
using System.Linq;

namespace Arena.Utilities
{
    public class ScoreList
    {
        private readonly Dictionary<BotClient.BotClient, List<GameScore>> _scoreDictionary;

        public ScoreList()
        {
            _scoreDictionary = new Dictionary<BotClient.BotClient, List<GameScore>>();
        }

        public void AddScore(BotClient.BotClient player, double score, BotClient.BotClient oponent)
        {
            if (_scoreDictionary.Count == 0 || !_scoreDictionary.Any(f => f.Key.Equals(player)))
            {
                var gameScoreList = new List<GameScore>
                {
                    new GameScore(oponent, score)
                };

                _scoreDictionary.Add(player, gameScoreList);
            }
            else
            {
               var playerRecord = _scoreDictionary.First(f => f.Key.Equals(player));
               var gameScore = new GameScore(oponent, score);

               playerRecord.Value.Add(gameScore);
            }
        }

        public List<GameScore> ShowGameScoresForPlayer(BotClient.BotClient bot)
        {
            return _scoreDictionary.FirstOrDefault(f => Equals(f.Key, bot)).Value;
        }

        public List<GameScore> ScoreAgainstPlayer(BotClient.BotClient bot, BotClient.BotClient oponent)
        {
            return _scoreDictionary.FirstOrDefault(f => f.Key == bot).Value.Where(f=>f.Oponent == oponent).ToList();
        }

        public void SaveScore(IDictionary<BotClient.BotClient, double> duelResoult)
        {
            foreach (var playerRecord in duelResoult)
            {
                var allOtherPlayers = duelResoult.Keys.Where(f => f != playerRecord.Key).ToList();

                foreach (var otherPlayer in allOtherPlayers)
                {
                    AddScore(playerRecord.Key, playerRecord.Value, otherPlayer);
                }
            }
        }
    }
}
