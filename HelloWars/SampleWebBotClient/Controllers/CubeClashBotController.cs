using System;
using System.Drawing;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;

namespace SampleWebBotClient.Controllers
{
    public class CubeClashBotController : ApiController
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
        public Point PerformNextMove(Point currentPoint)
        {
            var point = new Point
            {
                X = currentPoint.X + _rand.Next(0, 2),
                Y = currentPoint.Y + _rand.Next(0, 2)
            };

            return point;
        }
    }
}
