using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DynaBlaster.Models
{
    public class ExplodableBase
    {
        public Point Location { get; set; }
        public int ExplosionRadius { get; set; }
        public bool IsExploded { get; set; }
    }
}
