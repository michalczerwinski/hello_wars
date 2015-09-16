using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common;
using Common.Helpers;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Commands
{
    public class WatchCommand : CommandBase
    {
        private CubeClashViewModel _viewModel;
        public WatchCommand(CubeClashViewModel viewModel)
            : base()
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.PlayersCollection.First().Watch();
        }
    }
}
