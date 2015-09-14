using Arena.ViewModels;

namespace Arena.Commands
{
    class LoadGameAndEliminationUserControlsOnLoadedControl : CommandBase
    {
        private MainWindowViewModel _viewModel;
        public LoadGameAndEliminationUserControlsOnLoadedControl(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.Elimination = _viewModel.ArenaConfiguration.Elimination;
            _viewModel.Game = _viewModel.ArenaConfiguration.Game;

            if (_viewModel.Elimination != null)
            {
                _viewModel.Elimination.Bots = _viewModel.Competitors;
                _viewModel.EliminationTypeControl = _viewModel.Elimination.GetVisualization(_viewModel.ArenaConfiguration.EliminationConfiguration);
            }
            if (_viewModel.Game != null)
            {
                _viewModel.GameTypeControl = _viewModel.Game.GetVisualisationUserControl(_viewModel.ArenaConfiguration.GameConfiguration);
            }
        }
    }
}
