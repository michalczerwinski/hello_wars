using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Helpers.TankBlaster;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Controllers
{
    public abstract class TankBlasterSimpleBotControllerBase : ApiController
    {
        protected abstract string Name { get; }

        [HttpGet]
        public virtual BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = Name,
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "TankBlaster"
            };
            bot.Description = "Hi, I am " + bot.Name + " and I would like to win this tournament.... haha haha haha.";

            return bot;
        }

        [HttpPost]
        public virtual BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterAiService();
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}