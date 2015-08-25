using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Interfaces;
using Arena.Models;

namespace Arena.Games.Tanks
{
    public class Tanks : IGame
    {
        public long RoundNumber { get; set; }
        private TankGameViewModel _viewModel;

        public bool PerformNextMove()
        {
            throw new NotImplementedException();
        }

        public List<Competitor> Competitors { get; set; }

        public UserControl GetVisualisation()
        {
            _viewModel = new TankGameViewModel();
            SetBattleFieldSize(100, 100);
            return new TankGameControl(_viewModel);
        }

        private void SetBattleFieldSize(int width, int heigth)
        {
            _viewModel.Width = width;
            _viewModel.Heigth = heigth;
        }


        public IGame CreateNewGame(List<Competitor> competitors)
        {
            throw new NotImplementedException();
        }
    }
}
