using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Game.DynaBlaster.Models;
using Point = System.Drawing.Point;

namespace Game.DynaBlaster.UserControls
{
    /// <summary>
    /// Interaction logic for DynaBlasterUserControl.xaml
    /// </summary>
    public partial class DynaBlasterUserControl : UserControl
    {
        public DynaBlasterGridControl BoardGrid;
        private readonly GameArena _arena;

        public DynaBlasterUserControl(GameArena arena)
        {
            InitializeComponent();
            _arena = arena;

            BoardGrid = new DynaBlasterGridControl();
            BoardGrid.Init(15,15);
            
            AddChild(BoardGrid);

            arena.ArenaChanged += OnArenaChange;
        }

        public void OnArenaChange(object sender, EventArgs args)
        {
            BoardGrid.Children.Clear();

            DisplayBoard();

            DisplayBots();

            DisplayBombs();

            DisplayExplosions();
        }

        private void DisplayExplosions()
        {
            foreach (var explosion in _arena.Explosions)
            {
                //do not display blast ray on top of tiles that survived explosion
                var displayBlastLocations = explosion.BlastLocations.Where(point => _arena.Board[point.X, point.Y] == BoardTile.Empty).ToList();

                var xRadius = displayBlastLocations.Count(point => point.Y == explosion.Center.Y);
                var yRadius = displayBlastLocations.Count(point => point.X == explosion.Center.X);

                var xExplosion = new Ellipse()
                {
                    Fill = new SolidColorBrush(Colors.Yellow),
                    Height = 15
                };
                xExplosion.SetValue(Grid.ColumnSpanProperty, xRadius);

                var yExplosion = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Yellow),
                    Width = 15
                };
                yExplosion.SetValue(Grid.RowSpanProperty, yRadius);

                BoardGrid.AddElement(xExplosion, displayBlastLocations.Min(point => point.X), explosion.Center.Y);
                BoardGrid.AddElement(yExplosion, explosion.Center.X, displayBlastLocations.Min(point => point.Y));

                //all regular tiles in blast locations are firtified tiles that have been reduced by explosion.
                //paint them orange during explosion animation so it is visible to user
                foreach (var point in explosion.BlastLocations.Where(point => _arena.Board[point.X, point.Y] == BoardTile.Regular))
                {
                    BoardGrid.AddElement(new Rectangle(){ Fill = new SolidColorBrush(Colors.DarkOrange) }, point.X, point.Y);
                }
            }
        }

        private void DisplayBombs()
        {
            foreach (var bomb in _arena.Bombs)
            {
                var elementToAdd = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = new SolidColorBrush(Colors.Black)
                };
                var textToAdd = new TextBlock()
                {
                    Text = bomb.RoundsUntilExplodes.ToString()
                };
                BoardGrid.AddElement(elementToAdd, bomb.Location.X, bomb.Location.Y);
                BoardGrid.AddElement(textToAdd, bomb.Location.X, bomb.Location.Y);
            }
        }

        private void DisplayBots()
        {
            foreach (var bot in _arena.Bots)
            {
                var elementToAdd = new Ellipse
                {
                    Fill = new SolidColorBrush(bot.Color)
                };
                BoardGrid.AddElement(elementToAdd, bot.Location.X, bot.Location.Y);
            }
        }

        private void DisplayBoard()
        {
            for (int i = 0; i < _arena.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _arena.Board.GetLength(0); j++)
                {
                    UIElement elementToAdd = null;

                    switch (_arena.Board[i,j])
                    {
                        case BoardTile.Empty:
                            break;
                        case BoardTile.Regular:
                            elementToAdd = new Rectangle()
                            {
                                Fill = new SolidColorBrush(Colors.SaddleBrown)
                            };
                            break;
                        case BoardTile.Fortified:
                            elementToAdd = new Rectangle()
                            {
                                Fill = new SolidColorBrush(Colors.Brown)
                            };
                            break;
                        case BoardTile.Indestructible:
                            elementToAdd = new Rectangle()
                            {
                                Fill = new SolidColorBrush(Colors.Gray)
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
    }
}
