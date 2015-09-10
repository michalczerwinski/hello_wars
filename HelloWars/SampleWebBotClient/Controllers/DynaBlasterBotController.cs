using System;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Controllers
{
    public class DynaBlasterBotController : ApiController
    {
        private static readonly Random _rand = new Random(DateTime.Now.Millisecond);

        [HttpGet]
        public BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = NameHelper.GetRandomName(),
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "DynaBlaster"
            };

            return bot;
        }

        [HttpPost]
        public BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            return new BotMove()
            {
                Direction = (MoveDirection)_rand.Next(4),
                ShouldDropBomb = _rand.Next(7) == 0
            };
        }
    }
}
