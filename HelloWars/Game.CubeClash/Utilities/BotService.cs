using System;
using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using Game.AntWars.Enums;
using Game.AntWars.Models;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Utilities
{
    public class BotService
    {
        private Random _rand = new Random(DateTime.Now.Millisecond);
        private AntWarsViewModel _antWarsViewModel;
        private IEnumerable<ICompetitor> _competitors;

        public BotService(AntWarsViewModel antWarsViewModel)
        {
            _antWarsViewModel = antWarsViewModel;
        }

        public void AddBots(IEnumerable<ICompetitor> competitors)
        {
            _competitors = competitors;

            foreach (var competitor in _competitors)
            {
                var viewModel = new AntViewModel();
                var antModel = new AntModel(viewModel, competitor);

                do
                {
                    antModel.X = _rand.Next(0, _antWarsViewModel.ColumnCount);
                    antModel.Y = _rand.Next(0, _antWarsViewModel.RowCount);
                } while (!IsMoveValid(antModel.X, antModel.Y));
 
                _antWarsViewModel.MovableObjectsCollection.Add(antModel);
            }
        }

        public Dictionary<ICompetitor, double> GetBotResults()
        {
            var result = new Dictionary<ICompetitor, double>();

            if (!_antWarsViewModel.MovableObjectsCollection.OfType<AntModel>().Any())
            {
                foreach (var competitor in _competitors)
                {
                    result.Add(competitor, 0);
                }
            }
            else if (_antWarsViewModel.MovableObjectsCollection.OfType<AntModel>().Count() == 1)
            {
                foreach (var competitor in _competitors)
                {
                    if (competitor.Id == _antWarsViewModel.MovableObjectsCollection.OfType<AntModel>().First().Id)
                    {
                        result.Add(competitor, 1);
                    }
                    else
                    {
                        result.Add(competitor, 0);
                    }
                }
            }

            return result;
        }

        private bool IsMoveValid(int newX, int newY)
        {
            if (_antWarsViewModel.BattlefieldObjectsCollection.Where(type => type.Type == UnmovableObjectTypes.Wood || type.Type == UnmovableObjectTypes.Rock).Any(ant => ant.X == newX && ant.Y == newY))
            {
                return false;
            }

            if ((newY > _antWarsViewModel.RowCount - 1) || (newY < 0)
                || (newX > _antWarsViewModel.ColumnCount - 1) || (newX < 0))
            {
                return false;
            }

            if (_antWarsViewModel.MovableObjectsCollection.Any(ant => ant.X == newX && ant.Y == newY))
            {
                return false;
            }

            return true;
        }

    }
}
