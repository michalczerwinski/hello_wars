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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game.CubeClash.ViewModels;

namespace Game.CubeClash.UserControls
{
    /// <summary>
    /// Interaction logic for CubeClashUserControl.xaml
    /// </summary>
    public partial class CubeClashUserControl 
    {
        private CubeClashViewModel _viewModel
        {
            get { return (CubeClashViewModel) DataContext; }
            set { DataContext = value; }
        }

        public CubeClashUserControl(CubeClashViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
    }
}
