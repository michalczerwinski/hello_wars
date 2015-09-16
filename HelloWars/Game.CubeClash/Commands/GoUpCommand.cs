using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Commands
{
    class GoUpCommand : CommandBase
    {
        private CubeClashViewModel _viewModel;
        public GoUpCommand(CubeClashViewModel viewModel)
            : base()
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.PlayersCollection.First().Up();
        }
    }
}
