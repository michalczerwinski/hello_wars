using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Game.DynaBlaster.UserControls
{
    public class DynaBlasterGridControl : Grid
    {
        public void Init(int xSize, int ySize, int tileSize)
        {
            for (int i = 0; i < xSize; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition(){Width = new GridLength(tileSize)});
            }

            for (int i = 0; i < ySize; i++)
            {
                RowDefinitions.Add(new RowDefinition(){Height = new GridLength(tileSize)});
            }
        }
        
        public void AddElement(UIElement element, int x, int y)
        {
            if(element == null)
                return;
            
            element.SetValue(ColumnProperty, x);
            element.SetValue(RowProperty, y);
            Children.Add(element);
        }
    }
}
