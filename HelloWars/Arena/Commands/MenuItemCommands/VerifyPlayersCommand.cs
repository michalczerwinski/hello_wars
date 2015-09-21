using Arena.ViewModels;
using Common;

namespace Arena.Commands.MenuItemCommands
{
    class VerifyPlayersCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public VerifyPlayersCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.Competitors.ForEach(competitor => competitor.Name = "Verifying...");
            _viewModel.AskForCompetitors(_viewModel.ArenaConfiguration.GameConfiguration.Type, _viewModel.Competitors);
        }
    }
}
