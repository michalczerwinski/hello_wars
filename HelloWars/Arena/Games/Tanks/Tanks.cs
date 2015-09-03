using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Arena.Interfaces;
using Game.Common.Attributes;
using Game.Common.Interfaces;

namespace Arena.Games.Tanks
{
    [GameType("Tanks")]
    public class Tanks : IGame
    {
        public IDictionary<ICompetitor, double> GetResoult()
        {
            throw new NotImplementedException();
        }

        public long RoundNumber { get; set; }
        private TankGameViewModel _viewModel;

        public bool PerformNextRound()
        {
            throw new NotImplementedException();
        }

        public List<ICompetitor> Competitors { get; set; }

        public UserControl GetVisualisation()
        {
            _viewModel = new TankGameViewModel();
            SetBattleFieldSize(100, 100);
            return new TankGameControl(_viewModel);
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

        private void SetBattleFieldSize(int width, int heigth)
        {
            _viewModel.Width = width;
            _viewModel.Heigth = heigth;
        }


        public IGame CreateNewGame(List<ICompetitor> bots)
        {
            throw new NotImplementedException();
        }
    }
}
