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
using Arena.Games.Tanks.ViewModels;

namespace Arena.Games.Tanks.Models
{
    /// <summary>
    /// Interaction logic for TankControl.xaml
    /// </summary>
    public partial class TankControl : UserControl
    {
        public TankControl(TankViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
