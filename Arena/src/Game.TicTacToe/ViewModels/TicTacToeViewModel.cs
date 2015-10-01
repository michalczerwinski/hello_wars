using System.Windows;
using Common.Utilities;
using Game.TicTacToe.Models;

namespace Game.TicTacToe.ViewModels
{
    public class TicTacToeViewModel : BindableBase
    {
        private BindableArray<BoardFieldSign> _board;
        private BindableArray<Visibility> _arrayOfHorizontalLines;
        private BindableArray<Visibility> _arrayOfVerticalLines;
        private BindableArray<Visibility> _arrayOfDiagonalLines;

        public BindableArray<BoardFieldSign> Board
        {
            get { return _board; }
            set { SetProperty(ref _board, value); }
        }

        public BindableArray<Visibility> ArrayOfHorizontalLines
        {
            get { return _arrayOfHorizontalLines; }
            set { SetProperty(ref _arrayOfHorizontalLines, value); }
        }
        public BindableArray<Visibility> ArrayOfVerticalLines
        {
            get { return _arrayOfVerticalLines; }
            set { SetProperty(ref _arrayOfVerticalLines, value); }
        }
        public BindableArray<Visibility> ArrayOfDiagonalLines
        {
            get { return _arrayOfDiagonalLines; }
            set { SetProperty(ref _arrayOfDiagonalLines, value); }
        }

        public TicTacToeViewModel()
        {
            InitializeArrays();
        }

        private void InitializeArrays()
        {
            Board = new BindableArray<BoardFieldSign>(3, 3);
            ArrayOfHorizontalLines = new BindableArray<Visibility>(3, 1);
            ArrayOfVerticalLines = new BindableArray<Visibility>(3, 1);
            ArrayOfDiagonalLines = new BindableArray<Visibility>(2, 1);

            HideAllItemsInArray(Board);
            HideAllItemsInArray(ArrayOfHorizontalLines);
            HideAllItemsInArray(ArrayOfVerticalLines);
            HideAllItemsInArray(ArrayOfDiagonalLines);
        }

        private void HideAllItemsInArray(BindableArray<Visibility> array)
        {
            for (int i = 0; i < array.XSize; i++)
            {
                for (int j = 0; j < array.YSize; j++)
                {
                    array[i, j] = Visibility.Collapsed;
                }
            }
        }

        private void HideAllItemsInArray(BindableArray<BoardFieldSign> array)
        {
            for (int i = 0; i < array.XSize; i++)
            {
                for (int j = 0; j < array.YSize; j++)
                {
                    array[i, j] = BoardFieldSign.Empty;
                }
            }
        }
    }
}
