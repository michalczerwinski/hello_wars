using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common.Helpers;

namespace Game.DynaBlaster.Models
{
    public class GameArena
    {
        public GameArena(int boardWidth, int boardHeight)
        {
            Bots = new List<DynaBlasterBot>();
            Bombs = new List<Bomb>();
            Explosions = new List<Explosion>();
            Missiles = new List<Missile>();
            Board = new BoardTile[boardWidth, boardHeight];
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

        public void Reset()
        {
            Bots.Clear();
            Bombs.Clear();
            Missiles.Clear();
            Explosions.Clear();
            Board = new BoardTile[Board.GetLength(0), Board.GetLength(1)];
            OnArenaChanged();
        }

        public GameArena ExportState()
        {
            var arena = new GameArena(Board.GetLength(0), Board.GetLength(1));

            Board.ForEveryElement((x, y, val) =>
            {
                arena.Board[x, y] = val;
            });

            arena.Bots = Bots.Select(bot => new DynaBlasterBot(bot)).ToList();
            arena.Bombs = Bombs.Select(bomb => new Bomb(bomb)).ToList();
            arena.Missiles = Missiles.Select(missile => new Missile(missile)).ToList();

            return arena;
        }

        public void ImportState(GameArena arena)
        {
            Board = arena.Board;
            Bots = arena.Bots;
            Bombs = arena.Bombs;
            Missiles = arena.Missiles;
        }
    }
}