using System;
using System.Collections.Generic;
using Common.Interfaces;
using Game.AntWars.Models;
using Game.AntWars.ViewModels;

namespace Game.AntWars.Utilities
{
    public static class BotBuilder
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        private static AntWarsViewModel _antWarsViewModel;

        public static void AddBots(IEnumerable<ICompetitor> competitors, AntWarsViewModel antWarsViewModel)
        {
            _antWarsViewModel = antWarsViewModel;

            foreach (var competitor in competitors)
            {
                var viewModel = new AntViewModel();

                var antModel = new AntModel(viewModel, competitor)
                {
                    X = _rand.Next(0, _antWarsViewModel.ColumnCount),
                    Y = _rand.Next(0, _antWarsViewModel.RowCount),
                };

                _antWarsViewModel.MovableObjectsCollection.Add(antModel);
            }
        }
    }
}
