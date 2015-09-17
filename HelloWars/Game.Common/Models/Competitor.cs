using System;
using Common.Interfaces;

namespace Common.Models
{
    public class Competitor : BindableBase, ICompetitor
    {
        private string _name;
        private string _avatarUrl;

        public Guid Id { get; set; }
        public string Url { get; set; }

        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public Competitor()
        {
        }

        public Competitor(ICompetitor competitor)
        {
            Id = competitor.Id;
            Url = competitor.Url;
            AvatarUrl = competitor.AvatarUrl;
            Name = competitor.Name;
        }
    }
}
