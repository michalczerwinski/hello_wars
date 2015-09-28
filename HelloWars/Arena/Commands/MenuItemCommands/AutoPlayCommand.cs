using System.Threading.Tasks;
using Arena.ViewModels;
using Common;
using Common.Helpers;

namespace Arena.Commands.MenuItemCommands
{
    public class AutoPlayCommand : CommandBase
    {
        protected readonly MainWindowViewModel _viewModel;

        public AutoPlayCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public async override void Execute(object parameter = null)
        {
            _viewModel.IsGameInProgress = true;
            while (_viewModel.Elimination.GetNextCompetitors() != null && _viewModel.IsGameInProgress)
            {
                await _viewModel.PlayNextGameAsync();
                await DelayHelper.DelayAsync(_viewModel.ArenaConfiguration.GameConfiguration.NextMatchDelay);
            }
            _viewModel.IsGameInProgress = false;
        }
    }
}
