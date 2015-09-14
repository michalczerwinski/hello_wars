using System;
using System.Web;
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
            var aiService = new DynaBlasterAiService();
            var previousLocations = DynaBlasterStorageHelper.GetBotLocationHistory(arenaInfo.BotId);
            var result = aiService.CalculateNextMove(arenaInfo, previousLocations);

            previousLocations.Add(result.Direction != null ? arenaInfo.BotLocation.AddDirectionMove(result.Direction.Value) : arenaInfo.BotLocation);

            return result;
        }
    }
}
