namespace SampleWebBotClient.Models
{
    public class BotClient
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public override bool Equals(object obj)
        {
            var bot = obj as BotClient;
            return string.Equals(Url, bot.Url) && string.Equals(Name, bot.Name) && string.Equals(AvatarUrl, bot.AvatarUrl);
        }
    }
}