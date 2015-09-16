using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Commands
{
    internal class GoDownCommand : CommandBase
    {
        private CubeClashViewModel _viewModel;

        public GoDownCommand(CubeClashViewModel viewModel)
            : base()
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.PlayersCollection.First().Down();

        }
    }
}
