using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Arena.Games.TicTacToe.Models;
using Arena.Games.TicTacToe.UserControls;
using Arena.Games.TicTacToe.ViewModels;
using Arena.Interfaces;
using Bot = BotClient.BotClient;

namespace Arena.Games.TicTacToe
{
    public partial class TicTacToe : IGame
    {
        private Player _player1;
        private Player _player2;
        private List<Bot> _competitors;
        protected TicTacToeViewModel TicTacToeViewModel;
        public long RoundNumber { get; set; }

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

        public UserControl GetVisualisation()
        {
            TicTacToeViewModel = new TicTacToeViewModel();
            return new TicTacToeUserControl(TicTacToeViewModel);
        }

        public IDictionary<Bot, double> GetResoult()
        {
            var result = new Dictionary<Bot, double>();

            double score = _player1.IsWinner ? 1.0 : 0.0;
            var bot = _competitors.First(f => f == _player1.Bot);
            result.Add(bot, score);

            score = _player2.IsWinner ? 1.0 : 0.0;
            bot = _competitors.First(f => f == _player2.Bot);
            result.Add(bot, score);

            return result;
        }

        private void InitializePlayers()
        {
            if (Competitors.Count == 2)
            {
                _player1 = new Player
                {
                    Bot = Competitors[0],
                    PlayerMovesArray = TicTacToeViewModel.ArrayOfX
                };

                _player2 = new Player
                {
                    Bot = Competitors[1],
                    PlayerMovesArray = TicTacToeViewModel.ArrayOfO
                };
            }
            else
            {
                throw new Exception("Competitors list should contain exact 2 players.");
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
    }
}
