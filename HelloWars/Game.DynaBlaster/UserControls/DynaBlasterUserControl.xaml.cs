using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Game.DynaBlaster.Helpers;
using Game.DynaBlaster.Models;
using Image = System.Windows.Controls.Image;

namespace Game.DynaBlaster.UserControls
{
    /// <summary>
    /// Interaction logic for DynaBlasterUserControl.xaml
    /// </summary>
    public partial class DynaBlasterUserControl : UserControl
    {
        public DynaBlasterGridControl BoardGrid;
        private readonly GameArena _arena;
        private readonly BitmapImage _bombImgSource;
        private readonly BitmapImage _missileImgSource;
        private readonly BitmapImage _regularTileImgSource;
        private readonly BitmapImage _fortifiedTileImgSource;
        private readonly BitmapImage _fortifiedTileBlastImgSource;
        private readonly BitmapImage _indestructibleTileImgSource;
        private readonly BitmapImage _mapBackgroundImgSource;
        private readonly BitmapImage _bombExplVerImgSource;
        private readonly BitmapImage _bombExplHorImgSource;
        private readonly MediaPlayer _mediaPlayer;

        private readonly int _tileSize;

        public DynaBlasterUserControl(GameArena arena)
        {
            InitializeComponent();

            _arena = arena;
            
            _bombImgSource = ResourceImageHelper.LoadImage(Properties.Resources.bomb);
            _missileImgSource = ResourceImageHelper.LoadImage(Properties.Resources.missile);
            _regularTileImgSource = ResourceImageHelper.LoadImage(Properties.Resources.regularTile);
            _fortifiedTileImgSource = ResourceImageHelper.LoadImage(Properties.Resources.fortifiedTile);
            _fortifiedTileBlastImgSource = ResourceImageHelper.LoadImage(Properties.Resources.fortifiedTileBlast);
            _indestructibleTileImgSource = ResourceImageHelper.LoadImage(Properties.Resources.indestructibleTile);
            _mapBackgroundImgSource = ResourceImageHelper.LoadImage(Properties.Resources.grass);
            _bombExplHorImgSource = ResourceImageHelper.LoadImage(Properties.Resources.bomb_expl_mid_hor);
            _bombExplVerImgSource = ResourceImageHelper.LoadImage(Properties.Resources.bomb_expl_mid_vert);

            BoardGrid = new DynaBlasterGridControl();
            BoardGrid.Init(_arena.Board.GetLength(0), _arena.Board.GetLength(1));
            BoardGrid.Background = new ImageBrush(_mapBackgroundImgSource);

            AddChild(BoardGrid);

            _tileSize = (int) Height/_arena.Board.GetLength(1);
            Width = _tileSize*_arena.Board.GetLength(0);

            arena.ArenaChanged += OnArenaChange;
        }

        public void OnArenaChange(object sender, EventArgs args)
        {
            BoardGrid.Children.Clear();

            DisplayBoard();

            DisplayBots();

            DisplayBombs();

            DisplayMissiles();

            DisplayExplosions();
        }

        private void DisplayExplosions()
        {
            foreach (var explosion in _arena.Explosions)
            {
                //do not display blast ray on top of tiles that survived explosion
                var displayBlastLocations = explosion.BlastLocations.Where(point => _arena.Board[point.X, point.Y] == BoardTile.Empty).ToList();

                var xLocations = displayBlastLocations.Where(point => point.Y == explosion.Center.Y);
                var yLocations = displayBlastLocations.Where(point => point.X == explosion.Center.X);

                foreach (var xLocation in xLocations)
                {
                    var xExplosion = new Image()
                    {
                        Source = _bombExplHorImgSource,
                    };
                    BoardGrid.AddElement(xExplosion, xLocation.X, xLocation.Y);
                }

                foreach (var yLocation in yLocations)
                {
                    var yExplosion = new Image
                    {
                        Source = _bombExplVerImgSource,
                    };
                    BoardGrid.AddElement(yExplosion, yLocation.X, yLocation.Y);
                }
                
                //all regular tiles in blast locations are actually fortified tiles that have been reduced by explosion.
                //paint them orange during explosion animation so it is visible to user
                foreach (var point in explosion.BlastLocations.Where(point => _arena.Board[point.X, point.Y] == BoardTile.Regular))
                {
                    BoardGrid.AddElement(new Image(){ Source = _fortifiedTileBlastImgSource }, point.X, point.Y);
                }
            }
        }

        private void DisplayBombs()
        {
            foreach (var bomb in _arena.Bombs)
            {
                var elementToAdd = new Image()
                {
                    Width = _tileSize * 0.75,
                    Height = _tileSize * 0.75,
                    Source = _bombImgSource
                };

                var textToAdd = new TextBlock()
                {
                    Text = bomb.RoundsUntilExplodes.ToString()
                };
                BoardGrid.AddElement(elementToAdd, bomb.Location.X, bomb.Location.Y);
                BoardGrid.AddElement(textToAdd, bomb.Location.X, bomb.Location.Y);
            }
        }

        private void DisplayMissiles()
        {
            foreach (var missile in _arena.Missiles)
            {
                var elementToAdd = new Image()
                {
                    Width = _tileSize * 0.75,
                    Height = _tileSize * 0.75,
                    Source = _missileImgSource,
                    RenderTransform = new RotateTransform(GetRotateAngle(missile.MoveDirection)),
                    RenderTransformOrigin = new System.Windows.Point(0.5, 0.5)
                };

                BoardGrid.AddElement(elementToAdd, missile.Location.X, missile.Location.Y);
            }
        }

        private void DisplayBots()
        {
            foreach (var bot in _arena.Bots)
            {
                var elementToAdd = new Image
                {
                    Source = bot.Image,
                    RenderTransform = new RotateTransform(GetRotateAngle(bot.LastDirection)),
                    RenderTransformOrigin = new System.Windows.Point(0.5, 0.5)
                };
                BoardGrid.AddElement(elementToAdd, bot.Location.X, bot.Location.Y);
            }
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < _arena.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _arena.Board.GetLength(1); j++)
                {
                    UIElement elementToAdd = null;

                    switch (_arena.Board[i,j])
                    {
                        case BoardTile.Empty:
                            break;
                        case BoardTile.Regular:
                            elementToAdd = new Image()
                            {
                                Source = _regularTileImgSource
                            };
                            break;
                        case BoardTile.Fortified:
                            elementToAdd = new Image()
                            {
                                Source = _fortifiedTileImgSource
                            };
                            break;
                        case BoardTile.Indestructible:
                            elementToAdd = new Image()
                            {
                                Source = _indestructibleTileImgSource
                            };
                            break;
                    }

                    if (elementToAdd != null)
                    {
                        BoardGrid.AddElement(elementToAdd, i, j);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Gets angle to rotate image assuming that original image is facing up
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private int GetRotateAngle(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Right:
                    return 90;
                case MoveDirection.Left:
                    return 270;
                case MoveDirection.Up:
                    return 0;
                case MoveDirection.Down:
                    return 180;
            }
            return 0;
        }
    }
}
