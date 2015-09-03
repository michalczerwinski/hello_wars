using System.Windows;
using Arena.Helpers;

namespace Arena.Games.TicTacToe.Models
{
    public class Player
    {
        public BotClient.BotClient Bot;
        public bool IsWinner;
        public BindableArray<Visibility> PlayerMovesArray; 
        public Point NextMove()
        {
            return WebClientHelper<Point>.GetResponseFromUrl(Bot.Url + "PerformNextMove");
        }
    }
}
