using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Games.Tanks.UserControls;
using Arena.Games.Tanks.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.Tanks
{
    public class Tanks : IGame
    {
        public IDictionary<Bot, double> GetResoult()
        {
            throw new NotImplementedException();
        }

        public long RoundNumber { get; set; }
        private TankGameViewModel _viewModel;

        public bool PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public List<Bot> Competitors { get; set; }

        public UserControl GetVisualisation()
        {
            _viewModel = new TankGameViewModel();
            SetBattleFieldSize(100, 100);
            return new TankGameUserControl(_viewModel);
        }

        private void SetBattleFieldSize(int width, int heigth)
        {
            _viewModel.Width = width;
            _viewModel.Heigth = heigth;
        }


        public IGame CreateNewGame(List<Bot> bots)
        {
            throw new NotImplementedException();
        }
    }
}
