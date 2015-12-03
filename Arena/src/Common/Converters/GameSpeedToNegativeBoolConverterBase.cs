using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Common.Models;

namespace Common.Converters
{
    public abstract class GameSpeedToNegativeBoolConverterBase : IValueConverter
    {
        protected abstract GameSpeedMode SpeedMode { get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameSpeedMode)
            {
                return (GameSpeedMode)value == SpeedMode ? false : true;
            }
            else
            {
                throw new InvalidOperationException("The value must be GameSpeedMode");
            }
               
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}
