using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.ViewModels;
using Arena.Views;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using Game.TankBlaster.Models;

namespace Arena.Commands.MenuItemCommands
{
    public class StopDuelCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public StopDuelCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override async void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = false;

            var competitors = _viewModel.Elimination.GetNextCompetitors();

            var window = new WinnerSelectionDialog(competitors);
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                var result = new RoundResult()
                {
                    FinalResult = competitors.ToDictionary(comp => comp, comp => comp.Id == window.SelectedWinner.Id ? 1.0 : 0.0),
                    IsFinished = true
                };
                await _viewModel.MakeEndGameConfiguration(result);
            }
            else
            {
                _viewModel.IsGamePaused = true;
            }
        }
    }
}
