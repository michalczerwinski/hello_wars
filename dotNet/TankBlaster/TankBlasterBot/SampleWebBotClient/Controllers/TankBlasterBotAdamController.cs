using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Controllers
{
    public class TankBlasterBotAdamController : TankBlasterSimpleBotControllerBase
    {
        protected override string Name
        {
            get { return "Adam"; }
        }

        protected override string AvatarUrl
        {
            get { return Url.Content("~/Content/AdamImg.png"); }
        }

        public override BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterSimpleAIService(false, true, 99999, 8);
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}