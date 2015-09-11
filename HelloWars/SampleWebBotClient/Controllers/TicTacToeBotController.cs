using System;
using System.Drawing;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;

namespace SampleWebBotClient.Controllers
{
    public class TicTacToeBotController : ApiController
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        [HttpGet]
        public BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = NameHelper.GetRandomName(),
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "CubeClash"
            };

            return bot;
        }

        [HttpPost]
        public Point PerformNextMove(TicTacToeBoardFieldType[,] board)
        {
            var point = new Point();
            do
            {
                point.X = _rand.Next(0, 3);
                point.Y = _rand.Next(0, 3);
            }
            while (board[point.X, point.Y] != TicTacToeBoardFieldType.Empty);

            return point;
        }
    }
}
