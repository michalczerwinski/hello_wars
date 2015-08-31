using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Arena.Games.TicTacToe
{
    public partial class TicTacToe
    {
        private static void DoNextMove(Point movePoint, BindableArray<Visibility> array)
        {
            array[(int)movePoint.X, (int)movePoint.Y] = Visibility.Visible;

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                      new Action(() => Thread.Sleep(150)));
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            var arrayO = TicTacToeViewModel.ArrayOfO;
            var arrayX = TicTacToeViewModel.ArrayOfX;

            return arrayO[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed
                   && arrayX[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed;
        }

        private bool IsSomeoneWon()
        {
            if (IsWon(TicTacToeViewModel.ArrayOfX))
            {
                _player1.IsWinner = true;
                return true;
            }

            if (IsWon(TicTacToeViewModel.ArrayOfO))
            {
                _player2.IsWinner = true;
                return true;
            }

            return false;
        }

        private bool IsWon(BindableArray<Visibility> array)
        {
            var diagonal1 = 0;
            var diagonal2 = 0;
            for (int i = 0; i < array.XSize; i++)
            {
                var xEsLine = 0;
                var yEsLine = 0;
                for (int j = 0; j < array.YSize; j++)
                {
                    if (array[i, j] == Visibility.Visible)
                    {
                        xEsLine++;
                    }

                    if (array[j, i] == Visibility.Visible)
                    {
                        yEsLine++;
                    }

                    if ((i == j) && array[j, i] == Visibility.Visible)
                    {
                        diagonal1++;
                    }

                    if ((i == j || ((i == 2) && (j == 0)) || ((i == 0) && (j == 2))) && array[i, j] == Visibility.Visible)
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
            var arrayO = TicTacToeViewModel.ArrayOfO;
            var arrayX = TicTacToeViewModel.ArrayOfX;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayO[i, j] = Visibility.Collapsed;
                    arrayX[i, j] = Visibility.Collapsed;
                }
            }
        }
    }
}
