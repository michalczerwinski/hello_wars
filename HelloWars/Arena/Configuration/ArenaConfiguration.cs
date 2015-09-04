using System.Collections.Generic;
using System.Xml.Serialization;
using Arena.Interfaces;
using Common.Helpers;

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

        [XmlIgnore]
        public IElimination Elimination
        {
            get { return _elimination ?? (_elimination = TypeHelper<IElimination>.GetType(EliminationType)); }
        }
    }
}
