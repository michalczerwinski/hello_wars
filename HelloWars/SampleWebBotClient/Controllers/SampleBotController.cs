using System;
using System.Drawing;
using System.Web.Http;
using SampleWebBotClient.Models;

namespace SampleWebBotClient.Controllers
{
    public class SampleBotController : ApiController
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        [HttpGet]
        public BotClient Info()
        {
            var bot = new BotClient
            {
                Url = "",
                Name = "Czesiek",
                AvatarUrl = "http://localhost:53886/Content/BotImg.png"
            };

            return bot;
        }

        [HttpGet]
        public Point PerformNextMove()
        {
            return new Point
            {
                X = _rand.Next(0, 3),
                Y = _rand.Next(0, 3),
            };
        }
    }
}
