using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWars.Models
{
    public class Player
    {
        public string Nick { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }

        public Stack<string> DuelsHistory { get; set; }

        public Tank Tank { get; set; }
    }
}
