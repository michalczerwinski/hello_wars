using System.Web.Http;
using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models;
using SampleWebBotClient.Models.CubeClash;

namespace SampleWebBotClient.Controllers
{
    public class CubeClashBotController : ApiController
    {
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
            var nextMove = EnumValueHelper<AvailableActions>.RandomEnumValue();

            var result = new CubeMove();

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
                    break;
                }
            }

            return result;
        }
    }
}
