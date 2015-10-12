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

        public override BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterSimpleAIService(false, false, 99999, 99999);
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}