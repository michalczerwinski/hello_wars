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
using Arena.EliminationTypes.TournamentLadder.Models;
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
            var competitors = ((TournamentLadderViewModel)DataContext).Competitors;

            InitiateFirstRound(competitors);



            //var line = new Polyline();
            //line.Points.Add(new Point(10, 10));
            //line.Points.Add(new Point(20, 10));
            //line.Points.Add(new Point(30, 20));
            //line.Stroke = Brushes.Blue;
            //line.StrokeThickness = 8;

            //line.SetValue(Grid.ColumnProperty, i + 1);

            //Grid1.Children.Add(line);

        }

        private void InitiateDuelPairs(List<WrappedCompetitor> competitors)
        {
            throw new NotImplementedException();
        }

        #region CanvasPart
        private async void InitiateFirstRound(List<WrappedCompetitor> competitors)
        {
            var startingNumberOfCompetitors = CalculateCompetitorCount(competitors);
            var competitorCount = CalculateCompetitorCount(competitors);
            var numberOfRound = (int)Math.Ceiling(Math.Log(competitorCount, 2));

            ((TournamentLadderViewModel)DataContext).DuelPairList = CreateDuelPairs(competitors);

            for (int i = 1; i <= numberOfRound; i++)
            {
                //await InitRoundView(i);

                //await PlayRound(i);

                //RefreshView

            }




            var enumerateCompetitors = competitors.ToArray();
            for (int j = 0; j < competitors.Count; j++)
            {
                if (enumerateCompetitors[j] != null)
                {
                    AddCompetitorToRound(enumerateCompetitors[j], j, 1, 4, 8, 8);
                }
            }

            for (int i = 0; i < competitorCount - competitors.Count; i++)
            {
                var competitorBot = new WrappedCompetitor(new Competitor());
                AddCompetitorToRound(competitorBot, competitors.Count + i, 1, 4, 8, 8);
            }



            for (int i = 0; i < 4; i++)
            {
                AddCompetitorToRound(enumerateCompetitors[i], i, 2, 4, 4, 8);
            }


            for (int i = 0; i < 2; i++)
            {
                AddCompetitorToRound(enumerateCompetitors[i], i, 3, 4, 2, 8);
            }

            for (int i = 0; i < 1; i++)
            {
                AddCompetitorToRound(enumerateCompetitors[i], i, 4, 4, 1, 8);
            }
        }

        private List<Tuple<WrappedCompetitor, WrappedCompetitor>> CreateDuelPairs(List<WrappedCompetitor> competitors)
        {
            var pairList = new List<Tuple<WrappedCompetitor, WrappedCompetitor>>();

            

            //for (var i = 0; i < competitors.Count; i++)
            //{
            //    if (competitors[i+1])


            //    var tuple = new Tuple<WrappedCompetitor, WrappedCompetitor>(competitors[i],competitors[++i]);
            //    pairList.Add(tuple);

            //}

            return pairList;
        }

        private Task InitRound(int i)
        {
            throw new NotImplementedException();
        }

        private static int CalculateCompetitorCount(List<WrappedCompetitor> competitors)
        {
            var competitorCount = competitors.Count;
            if (competitorCount < 3) competitorCount = 2;
            if (competitorCount > 2 && competitorCount < 5)
            {
                competitorCount = 4;
            }
            if (competitorCount > 4 && competitorCount < 9)
            {
                competitorCount = 8;
            }
            if (competitorCount > 8 && competitorCount < 17)
            {
                competitorCount = 16;
            }
            if (competitorCount > 16 && competitorCount < 33)
            {
                competitorCount = 32;
            }
            if (competitorCount > 32 && competitorCount < 65)
            {
                competitorCount = 32;
            }
            return competitorCount;
        }

        //competitor should start from 0
        private void AddCompetitorToRound(WrappedCompetitor competitor, int orderInRow, int roundNumber, int roundCount, int competitorCount, int maxCompetitors)
        {
            var competitorView = new CompetitorViewControl();

            competitorView.DataContext = competitor.Competitor;

            competitor.CompetitorHeadPoint = new Point
            {
                X = ((roundNumber - 1) * 160) + 160,
                Y = ((maxCompetitors / competitorCount) * 60 * orderInRow + (Math.Pow(2, (roundNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            Canvas.SetTop(competitorView, (maxCompetitors / competitorCount) * 60 * orderInRow + (Math.Pow(2, (roundNumber - 1)) - 1) * 60 / 2);
            Canvas.SetLeft(competitorView, (roundNumber - 1) * 160);
            TournamentLadderCanvas.Children.Add(competitorView);
        }
        #endregion

        //private void CreateTournamentLadderGrid(List<Competitor> competitors)
        //{
        //    var competitorCount = competitors.Count;
        //    if (competitorCount < 3) competitorCount = 2;
        //    if (competitorCount > 2 && competitorCount < 5) { competitorCount = 4; }
        //    if (competitorCount > 4 && competitorCount < 9) { competitorCount = 8; }
        //    if (competitorCount > 8 && competitorCount < 17) { competitorCount = 16; }
        //    if (competitorCount > 16 && competitorCount < 33) { competitorCount = 32; }

        //    var columnCount = (int)Math.Ceiling(Math.Log(competitorCount, 2)) * 2 + 1;

        //    TournamentLadderGrid.ShowGridLines = true;

        //    for (int i = 0; i < columnCount; i = i + 2)
        //    {
        //        var competitorsColumnDefinition = new ColumnDefinition();
        //        var linesColumnDefinition = new ColumnDefinition();
        //        TournamentLadderGrid.ColumnDefinitions.Add(competitorsColumnDefinition);
        //        TournamentLadderGrid.ColumnDefinitions.Add(linesColumnDefinition);

        //        var newGrid = new Grid();
        //        newGrid.SetValue(Grid.ColumnProperty, i);
        //        newGrid.ShowGridLines = true;

        //        for (int j = 0; j < competitorCount; j++)
        //        {
        //            var rowDefinition = new RowDefinition();
        //            newGrid.RowDefinitions.Add(rowDefinition);

        //        }

        //        TournamentLadderGrid.Children.Add(newGrid);
        //        competitorCount = competitorCount / 2;
        //    }
        //}
    }
}