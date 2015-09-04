using System.Windows;
using Common.Interfaces;
using Game.TicTacToe.Models;
using Point = System.Drawing.Point;

namespace Game.TicTacToe.Interfaces
{
    public interface ITicTacToeBot : IBotClient<TicTacToeBoardFieldType[,], Point>
    {
        bool IsWinner { get; set; }
        BindableArray<Visibility> PlayerMovesArray { get;set; }
        TicTacToeBoardFieldType PlayerSign { get; set; }
    }
}