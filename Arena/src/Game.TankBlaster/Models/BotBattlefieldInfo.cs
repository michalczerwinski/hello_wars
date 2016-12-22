using System;
using System.Collections.Generic;
using System.Drawing;
using Game.TankBlaster.Interfaces;

namespace Game.TankBlaster.Models
{
    public class BotBattlefieldInfo
    {
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public Guid BotId { get; set; }
        public BoardTile[,] Board { get; set; }
        public Point BotLocation { get; set; }
        public int MissileAvailableIn { get; set; }
        public List<Point> OpponentLocations { get; set; }
        public List<IBomb> Bombs { get; set; }
        public List<IMissile> Missiles { get; set; }
        public TankBlasterConfig GameConfig { get; set; }
    }
}