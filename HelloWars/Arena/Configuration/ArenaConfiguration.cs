using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using Arena.Interfaces;

namespace Arena.Configuration
{
    [XmlRoot(ElementName = "ArenaConfiguration")]
    public class ArenaConfiguration
    {
        public string EliminationType { get; set; }

        public string GameType { get; set; }

        [XmlArrayItem(ElementName = "Url")]
        public List<string> CompetitorUrls { get; set; }

        private IElimination _elimination;
        private IGameProvider _gameDescription;

        [XmlIgnore]
        public IGameProvider GameDescription
        {
            get
            {
                if (_gameDescription == null)
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var gameType = assembly.GetType(GameType);
                    _gameDescription = (IGameProvider)Activator.CreateInstance(gameType);
                }

                return _gameDescription;
            }
        }

        [XmlIgnore]
        public IElimination Eliminations
        {
            get
            {
                if (_elimination == null)
                {
                    var eliminationType = Assembly.GetExecutingAssembly().GetType(EliminationType);
                    _elimination = (IElimination)Activator.CreateInstance(eliminationType);
                }

                return _elimination;
            }
        }
    }
}
