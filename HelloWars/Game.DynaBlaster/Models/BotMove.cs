using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DynaBlaster.Models
{
    public class BotMove
    {
        public MoveDirection Direction { get; set; }
        public bool ShouldDropBomb { get; set; }
    }
}
