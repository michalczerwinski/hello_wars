using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Games.PickTheWinner.UserControls;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.PickTheWinner
{
    public class PickTheWinner : IGame
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private List<Bot> _bots;
        private Dictionary<Bot, double> _result;
        public long RoundNumber { get; set; }

        public List<Bot> Competitors
        {
            get { return _bots; }
            set
            {
                _bots = value;
                if (value.Count != 0)
                {
                    CreateNewGame();
                }
            }
        }

        public IDictionary<Bot, double> GetResoult()
        {
            return _result;
        }

        public bool PerformNextRound()
        {
            var looser = _rand.Next(0, 2);
            _result = new Dictionary<Bot, double>();

            if (looser == 0)
            {
                _result.Add(Competitors[0], 0);
                _result.Add(Competitors[1], 1);
            }
            else
            {
                _result.Add(Competitors[0], 1);
                _result.Add(Competitors[1], 0);
            }
            RoundNumber++;
            return false;
        }

        public UserControl GetVisualisation()
        {
            return new PickTheWinnerControl();
        }

        public void CreateNewGame()
        {
            RoundNumber = 0;
        }
    }
}
