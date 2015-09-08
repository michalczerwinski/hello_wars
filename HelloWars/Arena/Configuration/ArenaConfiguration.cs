using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Serialization;
using Common.Interfaces;

namespace Arena.Configuration
{
    [XmlRoot(ElementName = "ArenaConfiguration")]
    public class ArenaConfiguration
    {
        public string EliminationType { get; set; }
        public string GameType { get; set; }

        [XmlArrayItem(ElementName = "Url")]
        public List<string> BotUrls { get; set; }

        [XmlIgnore]
        [ImportMany((typeof(IGame)))]
        public IEnumerable<IGame> GamePlugins;

        [XmlIgnore]
        [ImportMany((typeof(IElimination)))]
        public IEnumerable<IElimination> EliminationPlugins;

        [XmlIgnore]
        public IGame Game
        {
            get
            {
                return GamePlugins.FirstOrDefault(f => (f.GetType().Name == GameType));
            }
        }

        [XmlIgnore]
        public IElimination Elimination
        {
            get
            {
                return EliminationPlugins.FirstOrDefault(f => (f.GetType().Name == EliminationType));
            }
        }
    }
}
