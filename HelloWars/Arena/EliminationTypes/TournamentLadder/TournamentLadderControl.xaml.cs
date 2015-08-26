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
        private readonly int _startingNumberOfBots;
        public List<BotViewControl> TournamentList;
        private TournamentLadderViewModel _viewModel;
        private int _numberOfStages;

        public TournamentLadderControl(TournamentLadderViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _startingNumberOfBots = _viewModel.Bots.Count;
            _numberOfStages = (int)Math.Ceiling(Math.Log(_startingNumberOfBots, 2)) + 1;

            CreateEmptyTournamentLadderView();
            AddBotsToTournamentList();
            SetCanvasSize();
        }

        private void SetCanvasSize()
        {
            TournamentLadderCanvas.Height = _startingNumberOfBots * 60;
            TournamentLadderCanvas.Width = _numberOfStages * (160 + 50);
        }

        private List<BotViewControl> CreateStageList(int stageNumber, int numberOfBots)
        {
            var result = new List<BotViewControl>();

            if (stageNumber == 1)
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var BotShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    BotShell.Id = i;
                    BotShell.NextStageTargetId = i / 2;
                    result.Add(BotShell);
                }
            }
            else if (stageNumber == _numberOfStages)
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var BotShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    BotShell.Id = i;
                    result.Add(BotShell);
                }
            }
            else
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var BotShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    BotShell.Id = i;
                    BotShell.NextStageTargetId = i / 2;
                    result.Add(BotShell);
                }
            }

            return result;
        }

        private void AddBotsToTournamentList()
        {
            var BotsEnumerable = _viewModel.Bots.GetEnumerator();
            foreach (var emptyBotShell in _viewModel.StageLists[0])
            {
                BotsEnumerable.MoveNext();
                emptyBotShell.ViewModel.BotClient = BotsEnumerable.Current;
            }
        }

        private void CreateEmptyTournamentLadderView()
        {
            _viewModel.StageLists = new List<List<BotViewControl>>();
            var numberOfBots = _startingNumberOfBots;
            var stageNumber = 1;

            while (numberOfBots > 0)
            {
                var list = CreateStageList(stageNumber, numberOfBots);
                _viewModel.StageLists.Add(list);

                stageNumber++;
                numberOfBots = numberOfBots / 2;
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
                var BotsInStage = _viewModel.StageLists[i];

                for (int j = 0; j < BotsInStage.Count; j++)
                {
                    DrawTournamentLines(BotsInStage[j], BotsInStage[++j]);
                }
            }
        }

        private void SetDuels(List<BotViewControl> list)
        {
            for (int i = 0; i < list.Count; i = i + 2)
            {
                list[i].PairWithId = list[i + 1].Id;
                list[i + 1].PairWithId = list[i].Id;
            }
        }

        private BotViewControl AddEmptyBotToStage(int stageNumber, int orderInRow, int numberOfBots)
        {
            var BotView = new BotViewControl(new BotControlViewModel());

            BotView.BotHeadPoint = new Point
            {
                X = ((stageNumber - 1) * (160 + 50)) + 160,
                Y = ((_startingNumberOfBots / numberOfBots) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            BotView.BotTailPoint = new Point
            {
                X = ((stageNumber - 1) * (160 + 50)),
                Y = ((_startingNumberOfBots / numberOfBots) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2) + 60 / 2,
            };

            Canvas.SetTop(BotView, (_startingNumberOfBots / numberOfBots) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2);
            Canvas.SetLeft(BotView, (stageNumber - 1) * (160 + 50));
            TournamentLadderCanvas.Children.Add(BotView);

            return BotView;
        }

        private void DrawTournamentLines(BotViewControl Bot1, BotViewControl Bot2)
        {
            var line = new Polyline();
            line.Stroke = Brushes.BlueViolet;
            line.StrokeThickness = 8;
            TournamentLadderCanvas.Children.Add(line);
            Point point;

            line.Points.Add(Bot1.BotHeadPoint);

            point = new Point
            {
                X = Bot1.BotHeadPoint.X + 25,
                Y = Bot1.BotHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = Bot1.BotHeadPoint.X + 25,
                Y = Bot2.BotHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = Bot2.BotHeadPoint.X,
                Y = Bot2.BotHeadPoint.Y
            };
            line.Points.Add(point);

            line = new Polyline();
            line.Stroke = Brushes.BlueViolet;
            line.StrokeThickness = 8;
            TournamentLadderCanvas.Children.Add(line);

            point = new Point
            {
                X = Bot1.BotHeadPoint.X + 25,
                Y = (Math.Abs(Bot2.BotHeadPoint.Y - Bot1.BotHeadPoint.Y) / 2) + Math.Min(Bot2.BotHeadPoint.Y, Bot1.BotHeadPoint.Y)
            };
            line.Points.Add(point);

            point = new Point
            {
                X = Bot1.BotHeadPoint.X + 50,
                Y = (Math.Abs(Bot2.BotHeadPoint.Y - Bot1.BotHeadPoint.Y) / 2) + Math.Min(Bot2.BotHeadPoint.Y, Bot1.BotHeadPoint.Y)
            };
            line.Points.Add(point);
        }
    }
}