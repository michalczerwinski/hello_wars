using System.Windows;
using Common.Interfaces;
using Game.TicTacToe.Models;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Interfaces
{
    public interface ITicTacToeBot : IBotClient<BoardFieldSign[,], Point>
    {
        bool IsWinner { get; set; }
        BoardFieldSign PlayerSign { get; set; }
    }
}