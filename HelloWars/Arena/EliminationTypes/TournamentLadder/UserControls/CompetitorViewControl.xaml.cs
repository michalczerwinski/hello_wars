using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder.UserControls
{
    /// <summary>
    /// Interaction logic for UserViewControl.xaml
    /// </summary>
    /// []
   // [DebuggerDisplay("DataContext : {DataContextDebugView}")]
    public partial class CompetitorViewControl : UserControl
    {
        public Point CompetitorHeadPoint { get; set; }
        public Point CompetitorTailPoint { get; set; }
     
        public CompetitorViewControl(CompetitorControlViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
