using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Game.Common.Interfaces;
using Game.TicTacToe.Interfaces;
using Game.TicTacToe.Models;
using Game.TicTacToe.UserControls;
using Game.TicTacToe.ViewModels;
using System.Drawing;
using Game.Common.Attributes;
using Game.Common.Helpers;
using Point = System.Drawing.Point;

namespace Game.TicTacToe
{
    [GameType("TicTacToe")]
    public class TicTacToe : IGame
    {
        private ITicTacToeBot _player1 { get { return _competitors[0]; } }
        private ITicTacToeBot _player2 { get { return _competitors[1]; } }
        private TicTacToeBoardFieldType[,] _board;
        private List<ITicTacToeBot> _competitors;
        protected TicTacToeViewModel TicTacToeViewModel;

        public long RoundNumber { get; set; }

        public TicTacToe()
        {
            Reset();
        }

        public List<ICompetitor> Competitors
        {
            get { return _competitors.Select(bot => bot as ICompetitor).ToList(); }
        }

        public UserControl GetVisualisation()
        {
            TicTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeUserControl(TicTacToeViewModel);
        }

        public IDictionary<ICompetitor, double> GetResults()
        {
            return _competitors.ToDictionary(toeBot => toeBot as ICompetitor, toeBot => toeBot.IsWinner ? 1.0 : 0.0);
        }

        public void AddCompetitor(ICompetitor competitor)
        {
            _competitors.Add(new TicTacToeLocalBot(competitor));
        }

        public void Start()
        {
            InitializePlayers();
            ClearTheBoard();
        }

        public void Reset()
        {
            _board = new TicTacToeBoardFieldType[3, 3];
            _competitors = new List<ITicTacToeBot>();
        }

        private void InitializePlayers()
        {
            if (_competitors.Count == 2)
            {
                _player1.PlayerMovesArray = TicTacToeViewModel.ArrayOfO;
                _player1.PlayerSign = TicTacToeBoardFieldType.O;

                _player2.PlayerMovesArray = TicTacToeViewModel.ArrayOfX;
                _player2.PlayerSign = TicTacToeBoardFieldType.X;
            }
            else
            {
                throw new Exception("Competitors list should contain exactly 2 players.");
            }
        }

        /// <summary>
        /// Return false if there is no next round, otherwise return true.
        /// </summary>
        /// <returns></returns>
        public bool PerformNextRound()
        {
            if (_player1 == null || _player2 == null) { throw new Exception("There are no players to perform next round."); }

            PlayerNextMove(_player1);
            if (IsPlayerWon(_player1))
            {
                return false;
            }

            PlayerNextMove(_player2);
            if (IsPlayerWon(_player2))
            {
                return false;
            }

            if (IsBoardFull())
            {
                ClearTheBoard();
            }

            return true;
        }

        private void PlayerNextMove(ITicTacToeBot player)
        {
            while (!IsBoardFull())
            {
                var move = player.NextMove(_board);
                if (IsNextMoveValid(move))
                {
                    player.PlayerMovesArray[move.X, move.Y] = Visibility.Visible;
                    _board[move.X, move.Y] = player.PlayerSign;
                    DelayHelper.Delay(250);
                    break;
                }
            }
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            return _board[movePoint.X, movePoint.Y] == TicTacToeBoardFieldType.Empty;
//            var arrayO = TicTacToeViewModel.ArrayOfO;
//            var arrayX = TicTacToeViewModel.ArrayOfX;
//
//            return arrayO[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed
//                   && arrayX[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed;
        }

        private bool IsPlayerWon(ITicTacToeBot player)
        {
            var array = player.PlayerMovesArray;
            var diagonal1 = 0;
            var diagonal2 = 0;
            var xLine = new int[3];
            var yLine = new int[3];

            for (int i = 0; i < array.XSize; i++)
            {
                for (int j = 0; j < array.YSize; j++)
                {
                    if (array[i, j] == Visibility.Visible)
                    {
                        xLine[i]++;
                        yLine[j]++;
                    }

                    if ((i == j) && array[j, i] == Visibility.Visible)
                    {
                        diagonal1++;
                    }

                    if ((((i == 1) && (j == 1)) || ((i == 2) && (j == 0)) || ((i == 0) && (j == 2))) && array[i, j] == Visibility.Visible)
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
                    if (TicTacToeViewModel.ArrayOfO[i, j] == Visibility.Collapsed && TicTacToeViewModel.ArrayOfX[i, j] == Visibility.Collapsed)
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
                    TicTacToeViewModel.ArrayOfO[i, j] = Visibility.Collapsed;
                    TicTacToeViewModel.ArrayOfX[i, j] = Visibility.Collapsed;
                }

                TicTacToeViewModel.ArrayOfVerticalLines[i, 0] = Visibility.Collapsed;
                TicTacToeViewModel.ArrayOfHorizontalLines[i, 0] = Visibility.Collapsed;
            }

            TicTacToeViewModel.ArrayOfDiagonalLines[0, 0] = Visibility.Collapsed;
            TicTacToeViewModel.ArrayOfDiagonalLines[1, 0] = Visibility.Collapsed;
        }
    }
}
