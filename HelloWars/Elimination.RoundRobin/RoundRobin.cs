using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Interfaces;
using Elimination.RoundRobin.UserControls;
using Elimination.RoundRobin.ViewModels;

namespace Elimination.RoundRobin
{
    public class RoundRobin : IElimination
    {
        public List<ICompetitor> Bots { get; set; }
        private RoundRobinViewModel _viewModel;
        private Stack<Tuple<ICompetitor, ICompetitor>> _duelPairs;

        public UserControl GetVisualization()
        {
            _viewModel = new RoundRobinViewModel(Bots);
            CreateDualPairs();
            return new RoundRobinUserControl(_viewModel);
        }

        private void CreateDualPairs()
        {
            _duelPairs = new Stack<Tuple<ICompetitor, ICompetitor>>();

            foreach (var bot1 in _viewModel.Bots)
            {
                foreach (var bot2 in _viewModel.Bots)
                {
                    if (bot1 != bot2)
                    {
                        var tuple = new Tuple<ICompetitor, ICompetitor>(bot1.Competitor, bot2.Competitor);
                        _duelPairs.Push(tuple);
                    }
                }
            }
        }

        public IList<ICompetitor> GetNextCompetitors()
        {
            var result = new List<ICompetitor>();

            if (_duelPairs.Count != 0)
            {
                var duelPair = _duelPairs.Pop();
                result.Add(duelPair.Item1);
                result.Add(duelPair.Item2);
                return result;
            }
            return null;
        }

        public void SetLastDuelResult(IDictionary<ICompetitor, double> resultDictionary)
        {
            if (resultDictionary != null)
            {
                foreach (var singleResult in resultDictionary)
                {
                    var botViewModel = ReturnWrappedBot(singleResult.Key);

                    if ((int)singleResult.Value == 1)
                    {
                        botViewModel.Wins++;
                    }

                    if ((int)singleResult.Value == 0)
                    {
                        botViewModel.Loses++;
                    }
                }
            }
        }

        private BotViewModel ReturnWrappedBot(ICompetitor competitor)
        {
            return _viewModel.Bots.FirstOrDefault(bot => bot.Competitor.Id == competitor.Id);
        }


        public string GetGameDescription()
        {
            return "Round-Robin Elimination";
        }
    }
}
