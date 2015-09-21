using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int RoundsBeforeIncreasingBlastRadius { get; set; }
        public bool IsFastMissileModeEnabled { get; set; }
    }
}
