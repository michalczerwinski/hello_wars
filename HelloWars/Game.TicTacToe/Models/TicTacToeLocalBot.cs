using System;
using System.Windows;
using Game.Common.Interfaces;
using Game.Common.Models;
using Game.TicTacToe.Interfaces;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Models
{
    public class TicTacToeLocalBot : BotLocalClient<TicTacToeBoardFieldType[,], Point>, ITicTacToeBot
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public TicTacToeLocalBot(ICompetitor competitor) : base(competitor)
        {
        }

        public bool IsWinner { get; set; }
        public BindableArray<Visibility> PlayerMovesArray { get; set; }
        public TicTacToeBoardFieldType PlayerSign { get; set; }

        public Point NextMove()
        {
            return new Point
            {
                X = _rand.Next(0, 3),
                Y = _rand.Next(0, 3),
            };       
        }

        public override Point NextMove(TicTacToeBoardFieldType[,] arenaInfo)
        {
            var point = new Point();
            do
            {
                point.X = _rand.Next(0, 3);
                point.Y = _rand.Next(0, 3);
            } 
            while (arenaInfo[point.X,point.Y] != TicTacToeBoardFieldType.Empty);

            return point;
        }
    }
}
