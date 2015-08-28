using System;
using System.Windows;

namespace Arena.Games.TicTacToe.Models
{
    public class Player
    {
        private Random _rand = new Random(DateTime.Now.Millisecond);

        public string UniqueKey;
        public string PlayerId;
        public bool IsWinner;
        public Point NextMove()
        {
            return new Point
            {
                X = _rand.Next(0, 3),
                Y = _rand.Next(0, 3),
            };       
        }
    }
}
