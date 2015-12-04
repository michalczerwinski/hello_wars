using System;
using System.Windows.Navigation;
using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class VerifyPlayersCommand : CommandBase
    {
        private static MainWindowViewModel _viewModel;

        public VerifyPlayersCommand(MainWindowViewModel viewModel) :base(_canExecute)
        {
            _viewModel = viewModel;
        }

        private static bool _canExecute(object param)
        {
            return !_viewModel.IsGameInProgress;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.Competitors.ForEach(competitor => competitor.Name = "Verifying...");
            _viewModel.AskForCompetitors(_viewModel.ArenaConfiguration.GameConfiguration.Type, _viewModel.Competitors);
        }
    }
}
