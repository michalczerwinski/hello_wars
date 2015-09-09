using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Game.TicTacToe.Interfaces;
using Game.TicTacToe.Models;
using Game.TicTacToe.UserControls;
using Game.TicTacToe.ViewModels;
using Common.Helpers;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using Point = System.Drawing.Point;

namespace Game.TicTacToe
{
    public class TicTacToe : IGame
    {
        private ITicTacToeBot _player1 { get { return _competitors[0]; } }
        private ITicTacToeBot _player2 { get { return _competitors[1]; } }
        private List<ITicTacToeBot> _competitors;
        protected TicTacToeViewModel TicTacToeViewModel;

        public TicTacToe()
        {
            Reset();
        }

        public UserControl GetVisualisationControl()
        {
            TicTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeUserControl(TicTacToeViewModel);
        }

        public void SetupNewGame(IEnumerable<ICompetitor> competitors)
        {
            Reset();

            foreach (var competitor in competitors)
            {
                ITicTacToeBot bot = !string.IsNullOrEmpty(competitor.Url) ? new TicTacToeWebBot(competitor) : new TicTacToeLocalBot(competitor);
                _competitors.Add(bot);
            }

            InitializePlayers();
            ClearTheBoard();
        }

        public void Reset()
        {
            if (TicTacToeViewModel != null)
            {
                TicTacToeViewModel.Board = new BindableArray<BoardFieldSign>(3, 3);
            }
            _competitors = new List<ITicTacToeBot>();
        }

        public RoundResult PerformNextRound()
        {
            if (_player1 == null || _player2 == null) { throw new Exception("There are no players to perform next round."); }

            var result = new RoundResult()
            {
                History = new List<RoundPartialHistory>(),
                IsFinished = false
            };

            foreach (var competitor in _competitors)
            {
                if (IsBoardFull())
                {
                    ClearTheBoard();
                }

                result.History.Add(PerformNextMove(competitor));

                DelayHelper.Delay(250);

                if (IsPlayerWon(competitor))
                {
                    result.IsFinished = true;
                    result.FinalResult = GetResults();
                    break;
                }
            }

            return result;
        }

        public void SetPreview(object boardState)
        {
            ClearTheBoard();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TicTacToeViewModel.Board[i, j] = ((BoardFieldSign[,])boardState)[i, j];
                }
            }
        }

        private Dictionary<ICompetitor, double> GetResults()
        {
            return _competitors.ToDictionary(toeBot => toeBot as ICompetitor, toeBot => toeBot.IsWinner ? 1.0 : 0.0);
        }

        private void InitializePlayers()
        {
            if (_competitors.Count == 2)
            {
                _player1.PlayerSign = BoardFieldSign.O;
                _player2.PlayerSign = BoardFieldSign.X;
            }
            else
            {
                throw new Exception("Competitors list should contain exactly 2 players.");
            }
        }

        private RoundPartialHistory PerformNextMove(ITicTacToeBot competitor)
        {
            var move = competitor.NextMove(TicTacToeViewModel.Board);
            string moveDescription;

            if (IsNextMoveValid(move))
            {
                TicTacToeViewModel.Board[move.X, move.Y] = competitor.PlayerSign;
                moveDescription = string.Format("{0} has marked field [{1},{2}] with {3}", competitor.Name, move.X, move.Y, competitor.PlayerSign);
            }
            else
            {
                moveDescription = string.Format("{0} has performed an illegal move {{{1}, {2}}. No action was performed\n", competitor.Name, move.X, move.Y);
            }

            return new RoundPartialHistory()
            {
                Caption = moveDescription,
                BoardState = ExportBoardState()
            };
        }

        private BoardFieldSign[,] ExportBoardState()
        {
            var result = new BoardFieldSign[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = TicTacToeViewModel.Board[i, j];
                }
            }

            return result;
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            return TicTacToeViewModel.Board[movePoint.X, movePoint.Y] == BoardFieldSign.Empty;
        }

        private bool IsPlayerWon(ITicTacToeBot player)
        {
            var array = TicTacToeViewModel.Board;
            var diagonal1 = 0;
            var diagonal2 = 0;
            var xLine = new int[3];
            var yLine = new int[3];

            for (int i = 0; i < array.XSize; i++)
            {
                for (int j = 0; j < array.YSize; j++)
                {
                    if (array[i, j] == player.PlayerSign)
                    {
                        xLine[i]++;
                        yLine[j]++;
                    }

                    if ((i == j) && array[j, i] == player.PlayerSign)
                    {
                        diagonal1++;
                    }

                    if ((((i == 1) && (j == 1)) || ((i == 2) && (j == 0)) || ((i == 0) && (j == 2))) && array[i, j] == player.PlayerSign)
                    {
                        diagonal2++;
                    }
                }

                if (diagonal1 == 3)
                {
                    TicTacToeViewModel.ArrayOfDiagonalLines[0, 0] = Visibility.Visible;
                    player.IsWinner = true;
                    DelayHelper.Delay(500);
                    return true;
                }
                if (diagonal2 == 3)
                {
                    TicTacToeViewModel.ArrayOfDiagonalLines[1, 0] = Visibility.Visible;
                    player.IsWinner = true;
                    DelayHelper.Delay(500);
                    return true;
                }
                for (int j = 0; j < 3; j++)
                {
                    if (xLine[j] == 3)
                    {
                        TicTacToeViewModel.ArrayOfHorizontalLines[j, 0] = Visibility.Visible;
                        player.IsWinner = true;
                        DelayHelper.Delay(500);
                        return true;
                    }
                    if (yLine[j] == 3)
                    {
                        TicTacToeViewModel.ArrayOfVerticalLines[j, 0] = Visibility.Visible;
                        player.IsWinner = true;
                        DelayHelper.Delay(500);
                        return true;
                    }
                }
            }

            player.IsWinner = false;
            return false;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (TicTacToeViewModel.Board[i, j] == BoardFieldSign.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void ClearTheBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TicTacToeViewModel.Board[i, j] = BoardFieldSign.Empty;
                }

                TicTacToeViewModel.ArrayOfVerticalLines[i, 0] = Visibility.Collapsed;
                TicTacToeViewModel.ArrayOfHorizontalLines[i, 0] = Visibility.Collapsed;
            }

            TicTacToeViewModel.ArrayOfDiagonalLines[0, 0] = Visibility.Collapsed;
            TicTacToeViewModel.ArrayOfDiagonalLines[1, 0] = Visibility.Collapsed;
        }
    }
}
