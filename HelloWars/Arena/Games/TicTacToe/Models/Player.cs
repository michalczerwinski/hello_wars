using System;
using System.Windows;
using Bot = BotClient.BotClient;

namespace Arena.Games.TicTacToe.Models
{
    public class Player
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public Bot Bot;
        public bool IsWinner;
        public BindableArray<Visibility> PlayerMovesArray; 
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
