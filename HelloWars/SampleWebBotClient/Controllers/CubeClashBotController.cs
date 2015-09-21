using System;
using System.Drawing;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.CubeClash;

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
        public CubeMove PerformNextMove(SurroundingAreaInfo arenaInfo)
        {
            var nextMove = EnumValueHelper<AvailableMoves>.RandomEnumValue();
            var result = new CubeMove()
            {
                Move = nextMove
            };

            return result;
        }
    }
}
