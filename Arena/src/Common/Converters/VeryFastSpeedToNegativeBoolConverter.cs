using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Converters
{
    public sealed class VeryFastSpeedToNegativeBoolConverter : GameSpeedToNegativeBoolConverterBase
    {
        protected override GameSpeedMode SpeedMode
        {
            get
            {
                return GameSpeedMode.VeryFast;
            }
        }
    }
}
