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

        public CubeUserControl()
        {
            InitializeComponent();
        }

        private void CubeUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //_viewModel.Color = new SolidColorBrush( Colors.BlueViolet);
        }
    }
}
