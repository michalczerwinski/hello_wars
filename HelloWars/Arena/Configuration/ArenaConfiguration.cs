using System.Collections.Generic;
using System.Xml.Serialization;

namespace Arena.Configuration
{
    [XmlRoot(ElementName = "ArenaConfiguration")]
    public class ArenaConfiguration
    {
        public string EliminationType { get; set; }
        public string GameType { get; set; }

        [XmlArrayItem(ElementName = "Url")]
        public List<string> BotUrls { get; set; }

    }
}
