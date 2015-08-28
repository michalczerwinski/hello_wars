using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Arena.Games.TicTacToe.Models;
using Arena.Games.TicTacToe.UserControls;
using Arena.Games.TicTacToe.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.TicTacToe
{
    public class TicTacToe : IGame
    {
        private TicTacToeViewModel _ticTacToeViewModel;
        public long RoundNumber { get; set; }
        private Player _player1;
        private Player _player2;
        private List<Bot> _competitors;

        public List<Bot> Competitors
        {
            get { return _competitors; }
            set
            {
                _competitors = value;
                InitializePlayers();
            }
        }

        private void InitializePlayers()
        {
            if (Competitors.Count == 2)
            {
                _player1 = new Player
                {
                    UniqueKey = Competitors[0].Url,
                    PlayerId = Competitors[0].Name
                };
                _player2 = new Player
                {
                    UniqueKey = Competitors[1].Url,
                    PlayerId = Competitors[1].Name
                };
            }
            else
            {
                throw new Exception("Competitors list should contain exact 2 players.");
            }
        }

        public UserControl GetVisualisation()
        {
            _ticTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeUserControl(_ticTacToeViewModel);
        }


        public bool PerformNextRound()
        {
            //communication with client
            //Deserialize XML for Move from player
            if (_player1 == null || _player2 == null) { throw new Exception("There are no players to perform next round."); }

            var move = _player1.NextMove();

            while (true)
            {
                if (IsNextMoveValid(move))
                {
                    DoNextMove(move, _ticTacToeViewModel.ArrayOfX);
                    break;
                }
                else
                {
                    move = _player1.NextMove();
                }
            }

            if (IsGameFinish()) { return true; }

            while (true)
            {
                if (IsNextMoveValid(move))
                {
                    DoNextMove(move, _ticTacToeViewModel.ArrayOfO);
                    break;
                }
                else
                {
                    move = _player2.NextMove();
                }
            }

            if (IsGameFinish()) { return true; }
            return false;
        }

        public IDictionary<Bot, double> GetResoult()
        {
            var result = new Dictionary<Bot, double>();

            if (_player1.IsWinner)
            {
                double score;
                if (_player1.IsWinner)
                {
                    score = 1.0;
                }
                else
                {
                    score = 0.0;
                }

                var bot = new Bot
                {
                    Name = _player1.UniqueKey,
                };

                result.Add(bot, score);
            }
            else if (_player2.IsWinner)
            {
                double score;
                if (_player2.IsWinner)
                {
                    score = 1.0;
                }
                else
                {
                    score = 0.0;
                }

                var bot = new Bot
                {
                    Name = _player1.UniqueKey,
                };

                result.Add(bot, score);
            }

            return result;
        }

        #region GameLogic
        private void DoNextMove(Point movePoint, BindableArray<Visibility> array)
        {
            array[(int)movePoint.X, (int)movePoint.Y] = Visibility.Visible;
        }

        private bool IsNextMoveValid(Point movePoint)
        {
            var arrayO = _ticTacToeViewModel.ArrayOfO;
            var arrayX = _ticTacToeViewModel.ArrayOfX;

            return arrayO[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed
                   && arrayX[(int)movePoint.X, (int)movePoint.Y] == Visibility.Collapsed;
        }

        private bool IsGameFinish()
        {
            if (IsThereAWinner(_ticTacToeViewModel.ArrayOfX))
            {
                _player1.IsWinner = true;
                return true;
            }

            if (IsThereAWinner(_ticTacToeViewModel.ArrayOfO))
            {
                _player2.IsWinner = true;
                return true;
            }

            return false;
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
                  

                    if (array[j, i] == Visibility.Visible)
                    {
                        yEsLine++;
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
