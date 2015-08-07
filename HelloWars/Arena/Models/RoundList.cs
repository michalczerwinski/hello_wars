using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Models
{
    public class RoundList
    {
        public List<DuelPair> PairList { get; set; }
        public int RoundNumber { get; set; }

        public RoundList()
        {
            PairList = new List<DuelPair>();
        }
    }
}
