using System;
using System.Collections.Generic;
using Common.Interfaces;
using Game.CubeClash.Models;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Utilities
{
    public static class BotBuilder
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        private static CubeClashViewModel _cubeClashViewModel;

        public static void AddBots(IEnumerable<ICompetitor> competitors, CubeClashViewModel cubeClashViewModel)
        {
            _cubeClashViewModel = cubeClashViewModel;

            foreach (var competitor in competitors)
            {
                var viewModel = new CubeViewModel();


                var cubeModel = new CubeModel(viewModel, competitor)
                {
                    X = _rand.Next(0, _cubeClashViewModel.ColumnCount),
                    Y = _rand.Next(0, _cubeClashViewModel.RowCount),
                };

                _cubeClashViewModel.MovableObjectsCollection.Add(cubeModel);
            }
        }
    }
}
