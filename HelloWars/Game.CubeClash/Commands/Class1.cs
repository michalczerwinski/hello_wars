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
    public class Class1Command : CommandBase
    {
        private CubeClashViewModel _viewModel;
        public Class1Command(CubeClashViewModel viewModel)
            : base()
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter = null)
        {
            //do
            //{
            //    _viewModel.PlayersCollection.First().X++ ;
            //    _viewModel.PlayersCollection.First().Y = 1;
            //    DelayHelper.Delay(100);
            //} while (_viewModel.PlayersCollection.First().X < 50);

            _viewModel.PlayersCollection.First().MovementShadowRotate += 90;
            _viewModel.PlayersCollection.First().IsAttacking = _viewModel.PlayersCollection.First().IsAttacking == "False" ? "True" : "False";
            //_viewModel.PlayersCollection.First().Attack();
        }
    }
}
