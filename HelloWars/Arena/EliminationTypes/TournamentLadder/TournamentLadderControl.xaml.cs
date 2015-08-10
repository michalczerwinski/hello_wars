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
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Models;
using Arena.ViewModels;

namespace Arena.EliminationTypes.TournamentLadder
{
    /// <summary>
    /// Interaction logic for TournamentLadderControl.xaml
    /// </summary>
    public partial class TournamentLadderControl : UserControl
    {
        public TournamentLadderControl(TournamentLadderViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void TournamentLadderControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            var competitorCount = ((TournamentLadderViewModel)DataContext).Competitors.Count;
            var roundNumbers = (int)Math.Ceiling(Math.Log(competitorCount, 2));

            Grid1.HorizontalAlignment = HorizontalAlignment.Left;
            Grid1.VerticalAlignment = VerticalAlignment.Center;
            Grid1.ShowGridLines = true;

            for (int i = 0; i < roundNumbers; i++)
            {
                var columndefinition = new ColumnDefinition();

                Grid1.ColumnDefinitions.Add(columndefinition);

                var newGrid = new Grid();
                newGrid.SetValue(Grid.ColumnProperty, i);

                for (int j = 0; j < competitorCount; j++)
                {
                    var rowDefinition = new RowDefinition();
                    newGrid.RowDefinitions.Add(rowDefinition);

                    var competitorView = new CompetitorViewControl();
                    competitorView.DataContext = ((TournamentLadderViewModel)DataContext).Competitors.First();
                    ((Competitor)competitorView.DataContext).Name = "asdasdadas";

                    competitorView.SetValue(Grid.ColumnProperty, i);
                    competitorView.SetValue(Grid.RowProperty, j);
                    //  competitorView.SetValue(Grid.RowSpanProperty, i + 1);

                    newGrid.Children.Add(competitorView);

                }
                Grid1.Children.Add(newGrid);
                competitorCount = competitorCount / 2;
            }
        }
    }
}
