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
        private string _name;
        private string _avatarUrl;

        public int TournamentId { get; set; }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }

        public string Url { get; set; }

        public bool StilInGame
        {
            get { return _stilInGame; }
            set { SetProperty(ref _stilInGame, value); }
        }
    }
}
