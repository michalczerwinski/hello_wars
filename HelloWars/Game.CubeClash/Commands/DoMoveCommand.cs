using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.Commands
{
    public class DoMoveCommand : CommandBase
    {
        private CubeClashViewModel _viewModel;
        public DoMoveCommand(CubeClashViewModel viewModel)
            : base()
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            _viewModel.Collection.First().X = 1;
            _viewModel.Collection.First().Y = 1;
        }
    }
}
