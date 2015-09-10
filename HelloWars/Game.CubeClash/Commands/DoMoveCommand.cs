using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Helpers;
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
            do
            {
                _viewModel.PlayersCollection.First().X++ ;
                _viewModel.PlayersCollection.First().Y = 1;
                DelayHelper.Delay(100);
            } while (_viewModel.PlayersCollection.First().X < 50);
        }
    }
}
