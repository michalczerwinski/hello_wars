using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Common.Utilities;

namespace Game.DynaBlaster.Models
{
    public class GameArena
    {
        public bool[,] Board { get; set; }
        public List<DynaBlasterBot> Bots { get; set; }
        public List<Bomb> Bombs { get; set; }
        public List<Point> Explosions { get; set; }

        public GameArena()
        {
            Bots = new List<DynaBlasterBot>();
            Bombs = new List<Bomb>();
            Explosions = new List<Point>();
            Board = new bool[15, 15];
        }

        public event EventHandler BoardChanged;

        public void OnBoardChanged()
        {
            if (BoardChanged != null)
            {
                BoardChanged(this, EventArgs.Empty);
            }
        }
    }
}
