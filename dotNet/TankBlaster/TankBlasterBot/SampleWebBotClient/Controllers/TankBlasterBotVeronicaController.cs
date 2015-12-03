using SampleWebBotClient.Helpers;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Controllers
{
    public class TankBlasterBotVeronicaController : TankBlasterSimpleBotControllerBase
    {
        protected override string Name
        {
            get { return "Veronica"; }
        }

        protected override string AvatarUrl
        {
            get { return Url.Content("~/Content/VeronicaImg.png"); }
        }

        public override BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterSimpleAIService(true, true, 7, 7);
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}