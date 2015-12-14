using Common.Utilities;
using Elimination.RoundRobin.ViewModels;

namespace Elimination.RoundRobin.Commands
{
    public class ResetCommand : CommandBase
    {
        private readonly RoundRobinViewModel _viewModel;

        public ResetCommand(RoundRobinViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            foreach (var bot in _viewModel.Bots)
            {
                bot.Wins = 0;
                bot.Loses = 0;
                bot.PlayAgainstId.Clear();
            }
        }
    }
}
