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

namespace Arena.Commands.MenuItemCommands
{
    public class StopDuelCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public StopDuelCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = false;

            var competitors = _viewModel.Game.GetCurrentCompetitors();

            var wnd = new WinnerSelectionWindow(competitors);
            wnd.ShowDialog();

            if (wnd.DialogResult.HasValue && wnd.DialogResult.Value)
            {
                var result = new RoundResult()
                {
                    FinalResult = competitors.ToDictionary(comp => comp, comp => comp.Id == wnd.SelectedWinner.Id ? 1.0 : 0.0),
                    IsFinished = true
                };
                _viewModel.MakeEndGameConfiguration(result);
            }
            else
            {
                _viewModel.IsGamePaused = true;
            }
        }
    }
}
