using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleWebBotClient.Helpers.TankBlaster;
using SampleWebBotClient.Models.TankBlaster;

namespace SampleWebBotClient.Controllers
{
    public class TankBlasterBotVeronicaController : TankBlasterSimpleBotControllerBase
    {
        protected override string Name
        {
            get { return "Veronica"; }
        }

        public override BotMove PerformNextMove(BotArenaInfo arenaInfo)
        {
            var aiService = new TankBlasterSimpleAIService(true, true, 7, 7);
            var result = aiService.CalculateNextMove(arenaInfo);

            return result;
        }
    }
}