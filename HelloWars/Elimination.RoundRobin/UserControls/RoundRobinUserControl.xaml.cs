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
using Elimination.RoundRobin.ViewModels;

namespace Elimination.RoundRobin.UserControls
{
    /// <summary>
    /// Interaction logic for RoundRobinUserControl.xaml
    /// </summary>
    public partial class RoundRobinUserControl
    {
        private RoundRobinViewModel _viewModel
        {
            get { return (RoundRobinViewModel)DataContext; }
            set { DataContext = value; }
        }

        public RoundRobinUserControl(RoundRobinViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }
    }
}
