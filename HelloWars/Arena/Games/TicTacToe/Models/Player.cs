using System.IO;
using System.Net;
using System.Windows;
using Newtonsoft.Json;

namespace Arena.Games.TicTacToe.Models
{
    public class Player
    {
        public BotClient.BotClient Bot;
        public bool IsWinner;
        public BindableArray<Visibility> PlayerMovesArray; 
        public Point NextMove()
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Accept", "application/json");
            var jsonSerializer = JsonSerializer.Create();
            var botJson2 = webClient.DownloadString(Bot.Url + "PerformNextMove");
            var reader = new JsonTextReader(new StringReader(botJson2));
            var deserializedBot = jsonSerializer.Deserialize<Point>(reader);

            return deserializedBot;
        }
    }
}
