using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Helpers.TankBlaster;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Controllers
{
    public class TankBlasterBotController : ApiController
    {
        [HttpGet]
        public BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = NameHelper.GetRandomName(),
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "TankBlaster"
            };
            bot.Description = "Hi, I am " + bot.Name + " and I would like to win this tournament.... haha haha haha.";

            return bot;
        }

        [HttpPost]
        public BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterAiService();
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}
