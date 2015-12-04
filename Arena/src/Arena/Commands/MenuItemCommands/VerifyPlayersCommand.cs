using Arena.ViewModels;
using Common.Utilities;

namespace Arena.Commands.MenuItemCommands
{
    class VerifyPlayersCommand : CommandBase
    {
        private readonly MainWindowViewModel _viewModel;

        public VerifyPlayersCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.Competitors.ForEach(competitor => competitor.Name = "Verifying...");
            _viewModel.AskForCompetitors(_viewModel.ArenaConfiguration.GameConfiguration.Type, _viewModel.Competitors);
        }

        public override bool CanExecute(object parameter = null)
        {
            return !_viewModel.IsGameInProgress;
        }
    }
}
