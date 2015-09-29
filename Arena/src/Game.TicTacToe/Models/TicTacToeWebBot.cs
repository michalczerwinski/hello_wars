using Common.Interfaces;
using Common.Models;
using Game.TicTacToe.Interfaces;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Models
{
    public class TicTacToeWebBot : BotClientBase<BoardFieldSign[,], Point>, ITicTacToeBot
    {
        public bool IsWinner { get; set; }
        public BoardFieldSign PlayerSign { get; set; }

        public TicTacToeWebBot(ICompetitor competitor)
            : base(competitor)
        {
        }
    }
}
