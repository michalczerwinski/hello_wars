using System.Xml.Serialization;

namespace Game.DynaBlaster.Models
{
    [XmlRoot("DynaBlasterConfig")]
    public class DynaBlasterConfig
    {
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int BombBlastRadius { get; set; }
        public int MissileBlastRadius { get; set; }
        public int RoundsBetweenMissiles { get; set; }
    }
}
