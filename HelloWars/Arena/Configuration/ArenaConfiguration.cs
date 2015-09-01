using System.Collections.Generic;
using System.Xml.Serialization;
using Arena.Helpers;
using Arena.Interfaces;

namespace Arena.Configuration
{
    [XmlRoot(ElementName = "ArenaConfiguration")]
    public class ArenaConfiguration
    {
        public string EliminationType { get; set; }
        public string GameType { get; set; }

        [XmlArrayItem(ElementName = "Url")]
        public List<string> BotUrls { get; set; }

        private IElimination _elimination;
        private IGame _game;

        [XmlIgnore]
        public IGame Game
        {
            get { return _game ?? (_game = TypeHelper<IGame>.GetType(GameType)); }
        }

        [XmlIgnore]
        public IElimination Elimination
        {
            get { return _elimination ?? (_elimination = TypeHelper<IElimination>.GetType(EliminationType)); }
        }
    }
}
