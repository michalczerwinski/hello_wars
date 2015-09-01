using System;
using System.Collections.Generic;
using System.Linq;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Commands
{
    public class AutoPlayCommand : CommandBase
    {
        private readonly IElimination _elimination;
        private readonly IGame _game;
        private Dictionary<Bot, Stack<Tuple<Bot, double>>> _scoreList;

        public AutoPlayCommand(IElimination elimination, IGame game, Dictionary<Bot, Stack<Tuple<Bot, double>>> scoreList)
        {
            _scoreList = scoreList;
            _elimination = elimination;
            _game = game;
        }

        public override void Execute(object parameter = null)
        {
            var nextCompetitors = _elimination.GetNextCompetitors();

            while (nextCompetitors != null)
            {
                _game.Competitors = nextCompetitors.ToList();

                while (_game.PerformNextRound())
                {
                }

                var duelResoult = _game.GetResoult();
                _elimination.SetLastDuelResult(duelResoult);
                SaveDuelResult(duelResoult);
                nextCompetitors = _elimination.GetNextCompetitors();
            }
        }

        private void SaveDuelResult(IDictionary<BotClient.BotClient, double> duelResoult)
        {
            if (_scoreList == null)
            {
                _scoreList = new Dictionary<Bot, Stack<Tuple<BotClient.BotClient, double>>>();
            }

            var duelResoultList = duelResoult.Select(item => new Tuple<Bot, double>(item.Key, item.Value)).ToList();

            foreach (var competitor in duelResoultList)
            {
                var scoreRecord = _scoreList.FirstOrDefault(f => f.Key == competitor.Item1);

                if (scoreRecord.Key == null)
                {
                    var tempList = duelResoultList.Where(f => f.Item1 != competitor.Item1);
                    var newStack = new Stack<Tuple<Bot, double>>(tempList);

                    _scoreList.Add(competitor.Item1, newStack);
                }
                else
                {
                    var tempList = duelResoultList.Where(f => f.Item1 != competitor.Item1);

                    foreach (var temp in tempList)
                    {
                        scoreRecord.Value.Push(temp);
                    }
                }
            }
        }
    }
}
