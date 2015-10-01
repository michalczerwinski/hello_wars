using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Serialization;
using Common.Interfaces;
using Common.Models;

namespace Arena.Configuration
{
    [XmlRoot(ElementName = "ArenaConfiguration")]
    public class ArenaConfiguration
    {
        [XmlArrayItem(ElementName = "Url")]
        public List<string> BotUrls { get; set; }
        public GameConfiguration GameConfiguration { get; set; }
        public EliminationConfiguration EliminationConfiguration { get; set; }

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
                return GamePlugins.FirstOrDefault(f => (f.GetType().Name == GameConfiguration.Type));
            }
        }

        [XmlIgnore]
        public IElimination Elimination
        {
            get
            {
                return EliminationPlugins.FirstOrDefault(f => (f.GetType().Name == EliminationConfiguration.Type));
            }
        }
    }
}
