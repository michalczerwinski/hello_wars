using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common.Interfaces;

namespace Game.Common.Models
{
    public class Competitor : ICompetitor
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        public Competitor()
        {}

        public Competitor(ICompetitor competitor)
        {
            Id = competitor.Id;
            Url = competitor.Url;
            AvatarUrl = competitor.AvatarUrl;
            Name = competitor.Name;
        }
    }
}
