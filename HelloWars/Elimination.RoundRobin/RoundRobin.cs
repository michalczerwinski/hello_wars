using System.Collections.Generic;
using System.Linq;
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

        public UserControl GetVisualization()
        {
            _viewModel = new RoundRobinViewModel(Bots);
            return new RoundRobinUserControl(_viewModel);
        }

        public IList<ICompetitor> GetNextCompetitors()
        {
            var result = new List<ICompetitor>();
            if (_viewModel.NumberOfRepeat < 0)
            {
                _viewModel.NumberOfRepeat = 0;
                return null;
            }

            do
            {
                foreach (var bot1 in Bots)
                {
                    var bot1ViewModel = ReturnWrappedBot(bot1);

                    foreach (var bot2 in Bots.Where(bot2 => (bot1.Id != bot2.Id) && !bot1ViewModel.PlayAgainstId.Contains(bot2.Id)))
                    {
                        result.Add(bot1);
                        result.Add(bot2);
                        return result;
                    }
                }

                _viewModel.NumberOfRepeat--;

                foreach (var bot in Bots)
                {
                    ReturnWrappedBot(bot).PlayAgainstId.Clear();
                }

            } while (_viewModel.NumberOfRepeat > 0);

            return null;
        }

        public void SetLastDuelResult(IDictionary<ICompetitor, double> resultDictionary)
        {
            if (resultDictionary != null)
            {
                foreach (var resultFirstBot in resultDictionary)
                {
                    var botViewModel1 = ReturnWrappedBot(resultFirstBot.Key);

                    if ((int)resultFirstBot.Value == 1)
                    {
                        botViewModel1.Wins++;
                    }

                    if ((int)resultFirstBot.Value == 0)
                    {
                        botViewModel1.Loses++;
                    }

                    var collection = resultDictionary.Where(f => f.Key.Id != resultFirstBot.Key.Id);
                    foreach (var resultSecondBot in collection)
                    {
                        var botViewModel2 = ReturnWrappedBot(resultSecondBot.Key);

                        botViewModel1.PlayAgainstId.Add(resultSecondBot.Key.Id);
                        botViewModel2.PlayAgainstId.Add(resultFirstBot.Key.Id);
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
