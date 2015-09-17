using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.DynaBlaster.Models;

namespace Game.DynaBlaster.Interfaces
{
    public interface IMissile
    {
        Point Location { get; set; }
        int ExplosionRadius { get; set; }
        MoveDirection MoveDirection { get; set; }
    }
}
