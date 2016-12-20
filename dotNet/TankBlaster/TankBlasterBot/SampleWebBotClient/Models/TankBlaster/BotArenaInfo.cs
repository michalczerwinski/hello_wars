using System;
using System.Collections.Generic;
using System.Drawing;

namespace SampleWebBotClient.Models.TankBlaster
{
    public class BotArenaInfo
    {
        public int RoundNumber { get; set; }
        public int TurnNumber { get; set; }
        public Guid BotId { get; set; }
        public BoardTile[,] Board { get; set; }
        public Point BotLocation { get; set; }
        public int MissileAvailableIn { get; set; }
        public List<Point> OpponentLocations { get; set; }
        public List<Bomb> Bombs { get; set; }
        public List<Missile> Missiles { get; set; }
        public TankBlasterConfig GameConfig { get; set; }
    }
}
