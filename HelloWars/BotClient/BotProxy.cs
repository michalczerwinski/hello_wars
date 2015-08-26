namespace BotClient
{
    public class BotProxy
    {
        // http://mccomputer/super-mc
        // http://mccomputer/super-mc/bot-info
        // http://mccomputer/super-mc/perform-next-move

        //TODO: remove, Temporary
        private readonly string _urlName;

        public BotProxy(string url)
        {
            _urlName = url;
        }

        public string GetAvatarUrl()
        {
            return @"/Assets/TempFoto.png";
        }

        public string GetName()
        {
            return _urlName;
        }

        public string GetGameType()
        {
            return "";
        }

        public object PerformNextMove(object boardDescription)
        {
            // 1 Serialize BordDescrioption to JSON 
            // 2 Call post methd http://mccomputer/super-mc/perform-next-move
            // 3 Deserialize and return result 

            return null;
        }
    }
}
