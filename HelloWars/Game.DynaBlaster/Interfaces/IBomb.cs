using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DynaBlaster.Interfaces
{
    public interface IBomb
    {
        Point Location { get; set; }
        int ExplosionRadius { get; set; }
        int RoundsUntilExplodes { get; set; }
    }
}
