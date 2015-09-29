namespace SampleWebBotClient.Models.AntWars
{
    public class BotMove
    {
        public AvailableActions Action { get; set; }
        public ActionDirections? ActionDirection { get; set; }
    }
}