using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SetElement(UIElement element, int x, int y)
        {
            ClearElements(x, y);
            AddElement(element,x,y);
        }

        public void ClearElements(int x, int y)
        {
            var elements = Children.OfType<UIElement>().Where(uiElement => (int)uiElement.GetValue(ColumnProperty) == x && (int)uiElement.GetValue(RowProperty) == y);
            var uiElements = elements.ToList();
            if (uiElements.Any())
            {
                uiElements.ToList().ForEach(element => Children.Remove(element));
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
