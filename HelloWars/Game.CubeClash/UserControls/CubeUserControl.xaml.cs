using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.UserControls
{
    /// <summary>
    /// Interaction logic for CubeUserControl.xaml
    /// </summary>
    public partial class CubeUserControl
    {
        private CubeViewModel _viewModel
        {
            get { return (CubeViewModel) DataContext; }
            set { DataContext = value; }
        }

        public static readonly RoutedEvent CustomTestEvent = EventManager.RegisterRoutedEvent(
            "CustomTest", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CubeUserControl));

        public event RoutedEventHandler CustomTest
        {
            add { AddHandler(CustomTestEvent, value); }
            remove { RemoveHandler(CustomTestEvent, value); }
        }

        public CubeUserControl()
        {
            InitializeComponent();
        }

        private void ConnectAnimationEffect_OnLoaded(object sender, RoutedEventArgs e)
        {
           
        }

  

        private void MoveAnimation_OnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExplodeAnimation_OnCompleted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AttackAnimation_OnCompleted(object sender, EventArgs e)
        {
            _viewModel.IsAttackingAnimationCompleated = true;
        }
    }
}
