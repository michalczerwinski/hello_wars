using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Arena.EliminationTypes.TournamentLadder.UserControls;

namespace Arena.EliminationTypes.TournamentLadder
{
    /// <summary>
    /// Interaction logic for TournamentLadderControl.xaml
    /// </summary>
    public partial class TournamentLadderControl : UserControl
    {
        private readonly int _startingNumberOfCompetitors;
        public List<CompetitorViewControl> TournamentList;
        private TournamentLadderViewModel _viewModel;
        private int _numberOfStages;

        public TournamentLadderControl(TournamentLadderViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _startingNumberOfCompetitors = _viewModel.Competitors.Count;
            _numberOfStages = (int)Math.Ceiling(Math.Log(_startingNumberOfCompetitors, 2)) + 1;

            CreateEmptyTournamentLadderView();
            AddCompetitorsToTournamentList();
            SetCanvasSize();
        }

        private void SetCanvasSize()
        {
            TournamentLadderCanvas.Height = _startingNumberOfCompetitors * 60;
            TournamentLadderCanvas.Width = _numberOfStages * (160 + 50);
        }

        private List<CompetitorViewControl> CreateStageList(int stageNumber, int numberOfCompetitors)
        {
            var result = new List<CompetitorViewControl>();

            if (stageNumber == 1)
            {
                for (int i = 0; i < numberOfCompetitors; i++)
                {
                    var competitorShell = AddEmptyCompetitorToStage(stageNumber, i, numberOfCompetitors);
                    competitorShell.Id = i;
                    competitorShell.NextStageTargetId = i / 2;
                    result.Add(competitorShell);
                }
            }
            else if (stageNumber == _numberOfStages)
            {
                for (int i = 0; i < numberOfCompetitors; i++)
                {
                    var competitorShell = AddEmptyCompetitorToStage(stageNumber, i, numberOfCompetitors);
                    competitorShell.Id = i;
                    result.Add(competitorShell);
                }
            }
            else
            {
                for (int i = 0; i < numberOfCompetitors; i++)
                {
                    var competitorShell = AddEmptyCompetitorToStage(stageNumber, i, numberOfCompetitors);
                    competitorShell.Id = i;
                    competitorShell.NextStageTargetId = i / 2;
                    result.Add(competitorShell);
                }
            }

            return result;
        }

        private void AddCompetitorsToTournamentList()
        {
            var competitorsEnumerable = _viewModel.Competitors.GetEnumerator();
            foreach (var emptyCompetitorShell in _viewModel.StageLists[0])
            {
                competitorsEnumerable.MoveNext();
                emptyCompetitorShell.ViewModel.BotClient = competitorsEnumerable.Current;
            }
        }

        private void CreateEmptyTournamentLadderView()
        {
            _viewModel.StageLists = new List<List<CompetitorViewControl>>();
            var numberOfCompetitors = _startingNumberOfCompetitors;
            var stageNumber = 1;

            while (numberOfCompetitors > 0)
            {
                var list = CreateStageList(stageNumber, numberOfCompetitors);
                _viewModel.StageLists.Add(list);

                stageNumber++;
                numberOfCompetitors = numberOfCompetitors / 2;
            }

            foreach (var stageList in _viewModel.StageLists)
            {
                if (stageList.Count > 1)
                {
                    SetDuels(stageList);
                }
            }

            for (int i = 0; i < stageNumber - 2; i++)
            {
                var competitorsInStage = _viewModel.StageLists[i];

                for (int j = 0; j < competitorsInStage.Count; j++)
                {
                    DrawTournamentLines(competitorsInStage[j], competitorsInStage[++j]);
                }
            }
        }

        private void SetDuels(List<CompetitorViewControl> list)
        {
            for (int i = 0; i < list.Count; i = i + 2)
            {
                list[i].PairWithId = list[i + 1].Id;
                list[i + 1].PairWithId = list[i].Id;
            }
        }

        private CompetitorViewControl AddEmptyCompetitorToStage(int stageNumber, int orderInRow, int numberOfCompetitors)
        {
            var competitorView = new CompetitorViewControl(new CompetitorControlViewModel());

            competitorView.CompetitorHeadPoint = new Point
            {
                X = ((stageNumber - 1) * (160 + 50)) + 160,
                Y = ((_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            competitorView.CompetitorTailPoint = new Point
            {
                X = ((stageNumber - 1) * (160 + 50)),
                Y = ((_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            Canvas.SetTop(competitorView, (_startingNumberOfCompetitors / numberOfCompetitors) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2);
            Canvas.SetLeft(competitorView, (stageNumber - 1) * (160 + 50));
            TournamentLadderCanvas.Children.Add(competitorView);

            return competitorView;
        }

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
    }
}