using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common.Attributes
{
    public class GameTypeAttribute : Attribute
    {
        public string Type { get; set; }

        public GameTypeAttribute(string type)
        {
            Type = type;
        }
    }
}
