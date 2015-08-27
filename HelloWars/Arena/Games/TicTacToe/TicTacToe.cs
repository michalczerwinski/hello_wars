using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Arena.Games.TicTacToe.Models;
using Arena.Games.TicTacToe.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.TicTacToe
{
    public class TicTacToe : IGame
    {
        private TicTacToeViewModel _ticTacToeViewModel;
        public List<Bot> Bots { get; set; }
        public long RoundNumber { get; set; }

        private bool Player1Win;
        private bool Player2Win;

        public UserControl GetVisualisation()
        {
            _ticTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeControl(_ticTacToeViewModel);
        }

        public bool PerformNextMove()
        {
            //Deserialize XML for Move from player

            var move = new Point();
            if (IsNextMoveValid(move))
            {
                DoNextMove(move);
            }

            return IsGameFinish();
        }

       
        public Dictionary<Bot, double> GetResoult()
        {
            throw new NotImplementedException();
        }

        #region GameLogic
        private void DoNextMove(Point movePoint)
        {
            var arrayO = _ticTacToeViewModel.ArrayOfO;
            var arrayX = _ticTacToeViewModel.ArrayOfX;

            //if (Player1)
            //{
            //    arrayO[(int) movePoint.X, (int) movePoint.Y] = Visibility.Visible;
            //}
            //else if (Player2)
            //{
            //    arrayX[(int)movePoint.X, (int)movePoint.Y] = Visibility.Visible;
            //}
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            var arrayO = _ticTacToeViewModel.ArrayOfO;
            var arrayX = _ticTacToeViewModel.ArrayOfX;

            return (arrayO[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed)
                   || (arrayX[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed);
        }

        private bool IsGameFinish()
        {
            Player1Win = IsThereAWinner(_ticTacToeViewModel.ArrayOfX);
            Player2Win = IsThereAWinner(_ticTacToeViewModel.ArrayOfO);

            return (Player1Win || Player2Win);
        }

        private bool IsThereAWinner(BindableArray<Visibility> array)
        {
            for (int i = 0; i < array.XSize; i++)
            {
                var xEsLine = 0;
                var yEsLine = 0;
                var diagonal1 = 0;
                var diagonal2 = 0;
                for (int j = 0; j < array.YSize; j++)
                {
                    if (array[i, j] == Visibility.Visible)
                    {
                        xEsLine++;
                    }
                    else
                    {
                        break;
                    }

                    if (array[j, i] == Visibility.Visible)
                    {
                        yEsLine++;
                    }
                    else
                    {
                        break;
                    }

                    //need Work For This - Diagonal
                    if ((i == j) && array[j, i] == Visibility.Visible)
                    {
                        diagonal1++;
                    }

                    if ((i == j || ((i == 3) && (j == 1)) || ((i == 1) && (j == 3))) && array[j, i] == Visibility.Visible)
                    {
                        diagonal2++;
                    }
                }

                if (xEsLine == 3 || yEsLine == 3 || diagonal1 == 3 || diagonal2 == 3)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
