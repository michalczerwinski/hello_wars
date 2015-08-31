using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Arena.Eliminations.TournamentLadder.ViewModels;
using Arena.Games.TicTacToe.Models;
using Arena.Games.TicTacToe.UserControls;
using Arena.Games.TicTacToe.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.TicTacToe
{
    public partial class TicTacToe : IGame
    {
        protected TicTacToeViewModel TicTacToeViewModel;
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
                if (_competitors != null)
                {
                    ClearTheBoard();
                }
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
            TicTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeUserControl(TicTacToeViewModel);
        }

        /// <summary>
        /// Return false if there is no next round, otherwise return true.
        /// </summary>
        /// <returns></returns>
        public bool PerformNextRound()
        {
            if (_player1 == null || _player2 == null) { throw new Exception("There are no players to perform next round."); }

            var move = _player1.NextMove();

            while (!IsBoardFull())
            {
                if (IsNextMoveValid(move))
                {
                    DoNextMove(move, TicTacToeViewModel.ArrayOfX);
                    break;
                }
                else
                {
                    move = _player1.NextMove();
                }
            }

            if (IsSomeoneWon())
            {
                return false;
            }

            while (!IsBoardFull())
            {
                if (IsNextMoveValid(move))
                {
                    DoNextMove(move, TicTacToeViewModel.ArrayOfO);
                    break;
                }
                else
                {
                    move = _player2.NextMove();
                }
            }

            if (IsSomeoneWon())
            {
                return false;
            }

            if (IsBoardFull() && !IsSomeoneWon())
            {
                ClearTheBoard();
                return true;
            }
            return true;
        }


        public IDictionary<Bot, double> GetResoult()
        {
            var result = new Dictionary<Bot, double>();

            double score = _player1.IsWinner ? 1.0 : 0.0;
            var bot = _competitors.First(f => f.Name == _player1.UniqueKey);
            result.Add(bot, score);

            score = _player2.IsWinner ? 1.0 : 0.0;
            bot = _competitors.First(f => f.Name == _player2.UniqueKey);
            result.Add(bot, score);

            return result;
        }
    }
}
