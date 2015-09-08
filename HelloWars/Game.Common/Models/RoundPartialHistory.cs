using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RoundPartialHistory
    {
        public string Caption { get; set; }
        public object BoardState { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}
