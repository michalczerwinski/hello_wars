using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Arena.Eliminations.TournamentLadder.ViewModels;

namespace Arena.Eliminations.TournamentLadder.UserControls
{
    /// <summary>
    /// Interaction logic for TournamentLadderControl.xaml
    /// </summary>
    public partial class TournamentLadderControl 
    {
        private readonly int _startingNumberOfBots;
        public List<BotUserControl> TournamentList;
        private readonly TournamentLadderViewModel _viewModel;
        private readonly int _numberOfStages;

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

        private List<BotUserControl> CreateStageList(int stageNumber, int numberOfBots)
        {
            var result = new List<BotUserControl>();

            if (stageNumber == 1)
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var botShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    botShell.Id = i;
                    botShell.NextStageTargetId = i / 2;
                    result.Add(botShell);
                }
            }
            else if (stageNumber == _numberOfStages)
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var botShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    botShell.Id = i;
                    result.Add(botShell);
                }
            }
            else
            {
                for (int i = 0; i < numberOfBots; i++)
                {
                    var botShell = AddEmptyBotToStage(stageNumber, i, numberOfBots);
                    botShell.Id = i;
                    botShell.NextStageTargetId = i / 2;
                    result.Add(botShell);
                }
            }

            return result;
        }

        private void AddBotsToTournamentList()
        {
            var botsEnumerable = _viewModel.Bots.GetEnumerator();
            foreach (var emptyBotShell in _viewModel.StageLists[0])
            {
                botsEnumerable.MoveNext();
                emptyBotShell.ViewModel.BotClient = botsEnumerable.Current;
            }
        }

        private void CreateEmptyTournamentLadderView()
        {
            _viewModel.StageLists = new List<List<BotUserControl>>();
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
                var botsInStage = _viewModel.StageLists[i];

                for (int j = 0; j < botsInStage.Count; j++)
                {
                    DrawTournamentLines(botsInStage[j], botsInStage[++j]);
                }
            }
        }

        private void SetDuels(List<BotUserControl> list)
        {
            for (int i = 0; i < list.Count; i = i + 2)
            {
                list[i].PairWithId = list[i + 1].Id;
                list[i + 1].PairWithId = list[i].Id;
            }
        }

        private BotUserControl AddEmptyBotToStage(int stageNumber, int orderInRow, int numberOfBots)
        {
            var botView = new BotUserControl(new BotViewModel())
            {
                BotHeadPoint = new Point
                {
                    X = ((stageNumber - 1)*(160 + 50)) + 160,
                    Y = ((_startingNumberOfBots/numberOfBots)*60*orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1)*60/2) + 60/2,
                },
                BotTailPoint = new Point
                {
                    X = ((stageNumber - 1)*(160 + 50)),
                    Y = ((_startingNumberOfBots/numberOfBots)*60*orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1)*60/2) + 60/2,
                }
            };

            Canvas.SetTop(botView, (_startingNumberOfBots / numberOfBots) * 60 * orderInRow + (Math.Pow(2, (stageNumber - 1)) - 1) * 60 / 2);
            Canvas.SetLeft(botView, (stageNumber - 1) * (160 + 50));
            TournamentLadderCanvas.Children.Add(botView);

            return botView;
        }

        private void DrawTournamentLines(BotUserControl bot1, BotUserControl bot2)
        {
            var line = new Polyline
            {
                Stroke = Brushes.BlueViolet,
                StrokeThickness = 8
            };
            TournamentLadderCanvas.Children.Add(line);
            line.Points.Add(bot1.BotHeadPoint);

            var point = new Point
            {
                X = bot1.BotHeadPoint.X + 25,
                Y = bot1.BotHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = bot1.BotHeadPoint.X + 25,
                Y = bot2.BotHeadPoint.Y
            };
            line.Points.Add(point);

            point = new Point
            {
                X = bot2.BotHeadPoint.X,
                Y = bot2.BotHeadPoint.Y
            };
            line.Points.Add(point);

            line = new Polyline
            {
                Stroke = Brushes.BlueViolet, 
                StrokeThickness = 8
            };
            TournamentLadderCanvas.Children.Add(line);

            point = new Point
            {
                X = bot1.BotHeadPoint.X + 25,
                Y = (Math.Abs(bot2.BotHeadPoint.Y - bot1.BotHeadPoint.Y) / 2) + Math.Min(bot2.BotHeadPoint.Y, bot1.BotHeadPoint.Y)
            };
            line.Points.Add(point);

            point = new Point
            {
                X = bot1.BotHeadPoint.X + 50,
                Y = (Math.Abs(bot2.BotHeadPoint.Y - bot1.BotHeadPoint.Y) / 2) + Math.Min(bot2.BotHeadPoint.Y, bot1.BotHeadPoint.Y)
            };
            line.Points.Add(point);
        }
    }
}