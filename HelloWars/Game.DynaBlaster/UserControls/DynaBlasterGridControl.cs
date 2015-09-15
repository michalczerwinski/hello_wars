using System.Windows;
using System.Windows.Controls;

namespace Game.DynaBlaster.UserControls
{
    public class DynaBlasterGridControl : Grid
    {
        public void Init(int xSize, int ySize)
        {
            SetValue(ShowGridLinesProperty, true);

            for (int i = 0; i < xSize; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < ySize; i++)
            {
                RowDefinitions.Add(new RowDefinition());
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
