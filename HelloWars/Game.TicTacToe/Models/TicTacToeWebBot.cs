using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common.Interfaces;
using Common.Models;
using Game.TicTacToe.Interfaces;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Models
{
    public class TicTacToeWebBot : BotClientBase<TicTacToeBoardFieldType[,], Point>, ITicTacToeBot
    {
        public bool IsWinner { get; set; }
        public BindableArray<Visibility> PlayerMovesArray { get; set; }
        public TicTacToeBoardFieldType PlayerSign { get; set; }

        public TicTacToeWebBot(ICompetitor competitor)
            : base(competitor)
        {
        }
    }
}
