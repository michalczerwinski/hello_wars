using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace Game.DynaBlaster.Models
{
    public class BotArenaInfo
    {
        public Guid BotId { get; set; }
        public BoardTile[,] Board { get; set; }
        public Point BotLocation { get; set; }
        public bool IsMissileAvailable { get; set; }
        public List<Point> OpponentLocations { get; set; }
        public List<Bomb> Bombs { get; set; }
        public List<Missile> Missiles { get; set; }
    }
}