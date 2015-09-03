using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Games.PickTheWinner.UserControls;
using Arena.Interfaces;
using Game.Common.Attributes;
using Game.Common.Interfaces;

namespace Arena.Games.PickTheWinner
{
    [GameType("PickTheWinner")]
    public class PickTheWinner : IGame
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        private List<ICompetitor> _bots;
        private Dictionary<ICompetitor, double> _result;
        public long RoundNumber { get; set; }

        public List<ICompetitor> Competitors
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

        public IDictionary<ICompetitor, double> GetResoult()
        {
            return _result;
        }

        public bool PerformNextRound()
        {
            var looser = _rand.Next(0, 2);
            _result = new Dictionary<ICompetitor, double>();

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

        public IDictionary<ICompetitor, double> GetResults()
        {
            throw new NotImplementedException();
        }

        public void AddCompetitor(ICompetitor competitor)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void CreateNewGame()
        {
            RoundNumber = 0;
        }
    }
}
