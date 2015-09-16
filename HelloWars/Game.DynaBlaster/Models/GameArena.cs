using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game.DynaBlaster.Models
{
    public class GameArena
    {
        public GameArena()
        {
            Bots = new List<DynaBlasterBot>();
            Bombs = new List<Bomb>();
            Explosions = new List<Explosion>();
            Missiles = new List<Missile>();
            Board = new BoardTile[15, 15];
        }

        public BoardTile[,] Board { get; set; }
        public List<DynaBlasterBot> Bots { get; set; }
        public List<Bomb> Bombs { get; set; }
        public List<Missile> Missiles { get; set; }
        public List<Explosion> Explosions { get; set; }

        public event EventHandler ArenaChanged;

        public void OnArenaChanged()
        {
            if (ArenaChanged != null)
            {
                ArenaChanged(this, EventArgs.Empty);
            }
        }
    }
}