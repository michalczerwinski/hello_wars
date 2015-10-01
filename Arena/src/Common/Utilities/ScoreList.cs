using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using Common.Models;

namespace Common.Utilities
{
    public class ScoreList
    {
        private readonly Dictionary<ICompetitor, List<GameScore>> _scoreDictionary;

        public ScoreList()
        {
            _scoreDictionary = new Dictionary<ICompetitor, List<GameScore>>();
        }

        public void AddScore(ICompetitor player, double score, ICompetitor oponent)
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

        public List<GameScore> ShowGameScoresForPlayer(ICompetitor competitor)
        {
            return _scoreDictionary.FirstOrDefault(f => f.Key.Id == competitor.Id).Value;
        }

        public List<GameScore> ScoreAgainstPlayer(ICompetitor competitor, ICompetitor oponent)
        {
            return _scoreDictionary.FirstOrDefault(f => f.Key.Id == competitor.Id).Value.Where(f=>f.Oponent.Id == oponent.Id).ToList();
        }

        public void SaveScore(IDictionary<ICompetitor, double> duelResoult)
        {
            foreach (var playerRecord in duelResoult)
            {
                var allOtherPlayers = duelResoult.Keys.Where(f => f.Id != playerRecord.Key.Id).ToList();

                foreach (var otherPlayer in allOtherPlayers)
                {
                    AddScore(playerRecord.Key, playerRecord.Value, otherPlayer);
                }
            }
        }
    }
}
