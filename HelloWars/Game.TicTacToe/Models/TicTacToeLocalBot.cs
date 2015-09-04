using System;
using Common.Interfaces;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Models
{
    public class TicTacToeLocalBot : TicTacToeWebBot
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        public TicTacToeLocalBot(ICompetitor competitor) : base(competitor)
        {
        }

        public override Point NextMove(BoardFieldSign[,] arenaInfo)
        {
            var point = new Point();
            do
            {
                point.X = _rand.Next(0, 3);
                point.Y = _rand.Next(0, 3);
            } 
            while (arenaInfo[point.X,point.Y] != BoardFieldSign.Empty);

            return point;
        }
    }
}
