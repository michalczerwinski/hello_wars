using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Arena.EliminationTypes.TournamentLadder.UserControls;
using Arena.Models;

namespace Arena.EliminationTypes.TournamentLadder
{
    /// <summary>
    /// Interaction logic for TournamentLadderControl.xaml
    /// </summary>
    public partial class TournamentLadderControl : UserControl
    {
        private int _startingNumberOfCompetitors;
        private List<CompetitorViewControl> _tournamentList;
        private TournamentLadderViewModel _viewModel;

        public TournamentLadderControl(TournamentLadderViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        private void TournamentLadderControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            _startingNumberOfCompetitors = _viewModel.Competitors.Count;
            var numberOfCompetitors = _startingNumberOfCompetitors;
            var numberOfRound = (int)Math.Ceiling(Math.Log(_startingNumberOfCompetitors, 2)) + 1;

            CreateEmptyTournamentLadderView();

            AddCompetitorsToTournamentList();
        }

        private List<CompetitorViewControl> CreateRoundList(int roundNumber, int numberOfCompetitors)
        {
            var result = new List<CompetitorViewControl>();

            if (roundNumber == 1)
            {
                for (int i = 0; i < numberOfCompetitors; i++)
                {
                    var competitorShell = AddEmptyCompetitorToRound(roundNumber, i, numberOfCompetitors);
                   ((CompetitorControlViewModel)competitorShell.DataContext).Id = i;
                    result.Add(competitorShell);
                }
            }
            else
            {
                for (int i = 0; i < numberOfCompetitors; i++)
                {
                    var competitorShell = AddEmptyCompetitorToRound(roundNumber, i, numberOfCompetitors);
                    ((CompetitorControlViewModel)competitorShell.DataContext).Id = i;
                    ((CompetitorControlViewModel)competitorShell.DataContext).ItemConnected1 = ((CompetitorControlViewModel)competitorShell.DataContext).Id * 2;
                    ((CompetitorControlViewModel)competitorShell.DataContext).ItemConnected2 = ((CompetitorControlViewModel)competitorShell.DataContext).Id * 2 + 1;
                    result.Add(competitorShell);
                }
            }

            return result;
        }

        private void AddCompetitorsToTournamentList()
        {
            var competitorsEnumerable = _viewModel.Competitors.GetEnumerator();
            foreach (var emptyCompetitorShell in _viewModel.RoundList[0])
            {
                competitorsEnumerable.MoveNext();
                ((CompetitorControlViewModel)emptyCompetitorShell.DataContext).Competitor = competitorsEnumerable.Current;
            }
        }

        private void CreateEmptyTournamentLadderView()
        {
            var numberOfCompetitors = _startingNumberOfCompetitors;

            _viewModel.RoundList = new List<List<CompetitorViewControl>>();

            var roundNumber = 1;
            while (numberOfCompetitors > 0)
            {
                var list = CreateRoundList(roundNumber, numberOfCompetitors);

                _viewModel.RoundList.Add(list);

                roundNumber++;
                numberOfCompetitors = numberOfCompetitors / 2;
            }

            for (int i = 0; i < roundNumber - 2; i++)
            {
                var competitorsInRound = _viewModel.RoundList[i];

                for (int j = 0; j < competitorsInRound.Count; j++)
                {
                    DrawTournamentLines(competitorsInRound[j], competitorsInRound[++j]);
                }
            }
        }

        private CompetitorViewControl AddEmptyCompetitorToRound(int roundNumber, int orderInRow, int numberOfCompetitors)
        {
            var competitorView = new CompetitorViewControl(new CompetitorControlViewModel());

            competitorView.CompetitorHeadPoint = new Point
            {
                X = ((roundNumber - 1) * (160 + 50)) + 160,
                Y = ((_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (roundNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            competitorView.CompetitorTailPoint = new Point
            {
                X = ((roundNumber - 1) * (160 + 50)),
                Y = ((_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (roundNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            Canvas.SetTop(competitorView, (_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (roundNumber - 1)) - 1) * 60 / 2);
            Canvas.SetLeft(competitorView, (roundNumber - 1) * (160 + 50));
            TournamentLadderCanvas.Children.Add(competitorView);

            return competitorView;
        }


        #region CanvasPart

        private void DrawTournamentLines(CompetitorViewControl competitor1, CompetitorViewControl competitor2)
        {
            var line = new Polyline();
            line.Stroke = Brushes.BlueViolet;
            line.StrokeThickness = 8;
            TournamentLadderCanvas.Children.Add(line);
            Point point;

            line.Points.Add(competitor1.CompetitorHeadPoint);

            point = new Point
            {
                X = competitor1.CompetitorHeadPoint.X + 25,
                Y = competitor1.CompetitorHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = competitor1.CompetitorHeadPoint.X + 25,
                Y = competitor2.CompetitorHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = competitor2.CompetitorHeadPoint.X,
                Y = competitor2.CompetitorHeadPoint.Y
            };
            line.Points.Add(point);

            line = new Polyline();
            line.Stroke = Brushes.BlueViolet;
            line.StrokeThickness = 8;
            TournamentLadderCanvas.Children.Add(line);

            point = new Point
            {
                X = competitor1.CompetitorHeadPoint.X + 25,
                Y = (Math.Abs(competitor2.CompetitorHeadPoint.Y - competitor1.CompetitorHeadPoint.Y) / 2) + Math.Min(competitor2.CompetitorHeadPoint.Y, competitor1.CompetitorHeadPoint.Y)
            };
            line.Points.Add(point);

            point = new Point
            {
                X = competitor1.CompetitorHeadPoint.X + 50,
                Y = (Math.Abs(competitor2.CompetitorHeadPoint.Y - competitor1.CompetitorHeadPoint.Y) / 2) + Math.Min(competitor2.CompetitorHeadPoint.Y, competitor1.CompetitorHeadPoint.Y)
            };
            line.Points.Add(point);
        }
        #endregion
    }
}