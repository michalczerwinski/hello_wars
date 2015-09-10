using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.Models;
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
            DataContext = arena;
            _arena = arena;

            BoardGrid = new DynaBlasterGridControl();
            BoardGrid.Init(15,15);
            
            AddChild(BoardGrid);

            arena.BoardChanged += OnBoardChange;
        }

        public void OnBoardChange(object sender, EventArgs args)
        {
            BoardGrid.Children.Clear();

            for (int i = 0; i < _arena.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _arena.Board.GetLength(0); j++)
                {
                    if (_arena.Board[i, j])
                    {
                        var elementToAdd = new Rectangle()
                        {
                            Fill = new SolidColorBrush(Colors.SaddleBrown)
                        };
                        BoardGrid.AddElement(elementToAdd, i, j);
                    }
                }
            }

            foreach (var bot in _arena.Bots)
            {
                var elementToAdd = new Ellipse
                {
                    Fill = new SolidColorBrush(bot.Color)
                };
                BoardGrid.AddElement(elementToAdd, bot.Location.X, bot.Location.Y);
            }

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

            foreach (var explosion in _arena.Explosions)
            {
                var xRayLocation = new Point(explosion.X - 2, explosion.Y);
                var yRayLocation = new Point(explosion.X, explosion.Y - 2);

                var xRadius = 5;
                var yRadius = 5;

                if (xRayLocation.X < 0)
                {
                    xRadius += xRayLocation.X;
                    xRayLocation.X = 0;
                }

                if (yRayLocation.Y < 0)
                {
                    yRadius += yRayLocation.Y;
                    yRayLocation.Y = 0;
                }

                var xExplosion = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Yellow)
                };
                xExplosion.SetValue(Grid.ColumnSpanProperty, xRadius);

                var yExplosion = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Yellow)
                };
                yExplosion.SetValue(Grid.RowSpanProperty, yRadius);

                BoardGrid.AddElement(xExplosion, xRayLocation.X, xRayLocation.Y);
                BoardGrid.AddElement(yExplosion, yRayLocation.X, yRayLocation.Y);
            }

        }


    }
}
