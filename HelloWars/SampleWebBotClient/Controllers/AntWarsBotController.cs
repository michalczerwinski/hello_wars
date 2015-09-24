using System.Configuration;
using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.AntWars;

namespace SampleWebBotClient.Controllers
{
    public class AntWarsBotController : ApiController
    {
        [HttpGet]
        public BotInfo Info()
        {
            var bot = new BotInfo()
            {
                Name = NameHelper.GetRandomName(),
                AvatarUrl = "http://localhost:53886/Content/BotImg.png",
                GameType = "AntWars"
            };
            bot.Description = "Hi, I am " + bot.Name + " and I would like to win this tournament.... haha haha haha.";

            return bot;
        }

        [HttpPost]
        public BotMove PerformNextMove(SurroundingAreaInfo arenaInfo)
        {
            var nextMove = EnumValueHelper<AvailableActions>.RandomEnumValue();

            var result = new BotMove();

            result.Action = nextMove;
            switch (nextMove)
            {
                case AvailableActions.FireMissile:
                case AvailableActions.Move:
                    {
                        result.ActionDirection = EnumValueHelper<ActionDirections>.RandomEnumValue();
                        break;
                    }

                case AvailableActions.Watch:
                    {
                        result.ActionDirection = null;
                        break;
                    }
            }

            return result;
        }
    }
}
