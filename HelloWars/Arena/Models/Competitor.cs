using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Models
{
    public class Competitor : BindableBase
    {
        private bool _stilInGame;
        
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Url { get; set; }

        public bool StilInGame
        {
            get { return _stilInGame; }
            set { SetProperty(ref _stilInGame, value); }
        }
    }
}
