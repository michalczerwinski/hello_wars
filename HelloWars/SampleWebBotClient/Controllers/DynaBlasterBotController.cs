using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Helpers.DynaBlaster;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.DynaBlaster;

namespace SampleWebBotClient.Controllers
{
    public class DynaBlasterBotController : ApiController
    {
        [HttpGet]
        public BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = NameHelper.GetRandomName(),
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "DynaBlaster"
            };
            bot.Description = "Hi, I am " + bot.Name + " and I would like to win this tournament.... haha haha haha.";

            return bot;
        }

        [HttpPost]
        public BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new DynaBlasterAiService();
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}
