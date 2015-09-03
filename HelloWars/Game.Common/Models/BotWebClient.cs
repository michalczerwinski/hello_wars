using Game.Common.Interfaces;

namespace Game.Common.Models
{
    public class BotWebClient<TArenaInfo, TMove> : Competitor, IBotClient<TArenaInfo, TMove>
    {
        public TMove NextMove(TArenaInfo arenaInfo)
        {
            throw new System.NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var bot = obj as BotWebClient<TArenaInfo, TMove>;
            return string.Equals(Url, bot.Url) && string.Equals(Name, bot.Name) && string.Equals(AvatarUrl, bot.AvatarUrl);
        }


    }
}
