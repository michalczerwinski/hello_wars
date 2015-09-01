using System.Windows;
using Arena.Games.TicTacToe.Models;
using Arena.Helpers;

namespace Arena.Games.TicTacToe
{
    public partial class TicTacToe
    {
        private static void DoNextMove(Point movePoint, BindableArray<Visibility> array)
        {
            array[(int)movePoint.X, (int)movePoint.Y] = Visibility.Visible;
            DelayHelper.Delay(250);
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            var arrayO = TicTacToeViewModel.ArrayOfO;
            var arrayX = TicTacToeViewModel.ArrayOfX;

            return arrayO[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed
                   && arrayX[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed;
        }

        private bool IsPlayerWon(Player player)
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
            }
            TicTacToeViewModel.ArrayOfDiagonalLines[0, 0] = Visibility.Collapsed;
            TicTacToeViewModel.ArrayOfDiagonalLines[1, 0] = Visibility.Collapsed;

            for (int i = 0; i < 3; i++)
            {
                TicTacToeViewModel.ArrayOfVerticalLines[i, 0] = Visibility.Collapsed;
                TicTacToeViewModel.ArrayOfHorizontalLines[i, 0] = Visibility.Collapsed;
            }
        }
    }
}
