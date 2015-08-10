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

        [XmlArrayItem(ElementName = "CompetitorUrl")]
        public List<string> Competitors { get; set; }

        private IElimination _elimination;
        private IGameDescription _gameDescription;

        [XmlIgnore]
        public IGameDescription GameDescription
        {
            get
            {
                if (_gameDescription == null)
                {
                    var sss = Assembly.GetExecutingAssembly();
                    var gameType = sss.GetType(GameType);
                    _gameDescription = (IGameDescription)Activator.CreateInstance(gameType);
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
                    _elimination.Competitors = Competitors;
                }

                return _elimination;
            }
        }
    }
}
