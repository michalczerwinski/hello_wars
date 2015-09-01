﻿using System.Windows;

namespace Arena.Games.TicTacToe.ViewModels
{
    public class TicTacToeViewModel : BindableBase
    {
        private BindableArray<Visibility> _arrayOfX;
        private BindableArray<Visibility> _arrayOfO;
        private BindableArray<Visibility> _arrayOfHorizontalLines;
        private BindableArray<Visibility> _arrayOfVerticalLines;
        private BindableArray<Visibility> _arrayOfDiagonalLines;

        public BindableArray<Visibility> ArrayOfO
        {
            get { return _arrayOfO; }
            set { SetProperty(ref _arrayOfO, value); }
        }
        public BindableArray<Visibility> ArrayOfX
        {
            get { return _arrayOfX; }
            set { SetProperty(ref _arrayOfX, value); }
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
            ArrayOfX = new BindableArray<Visibility>(3, 3);
            ArrayOfO = new BindableArray<Visibility>(3, 3);
            ArrayOfHorizontalLines = new BindableArray<Visibility>(3, 1);
            ArrayOfVerticalLines = new BindableArray<Visibility>(3, 1);
            ArrayOfDiagonalLines = new BindableArray<Visibility>(2, 1);

            HideAllItemsInArray(ArrayOfX);
            HideAllItemsInArray(ArrayOfO);
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
    }
}
