using System;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Interfaces;

namespace Common.Models
{
    public class Competitor : BindableBase, ICompetitor
    {
        private string _name;
        private string _avatarUrl;
        private string _description;
        public Guid Id { get; set; }
        public string Url { get; set; }

        public bool IsVerified { get; private set; }

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

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public Competitor()
        {
        }

        public Competitor(ICompetitor competitor)
        {
            Id = competitor.Id;
            Url = competitor.Url;
            Description = competitor.Description;
            AvatarUrl = competitor.AvatarUrl;
            Name = competitor.Name;
        }

        public async Task VerifyAsync(string gameType)
        {
            IsVerified = false;
            var competitorInfo = await LoadCompetitorAsync();

            if (competitorInfo.GameType != gameType)
            {
                throw new ArgumentException("Game type mismatch");
            }

            Name = competitorInfo.Name;
            AvatarUrl = competitorInfo.AvatarUrl;
            Description = competitorInfo.Description;
            IsVerified = true;
        }

        private async Task<CompetitorInfo> LoadCompetitorAsync()
        {
            return await WebClientHelper.GetDataAsync<CompetitorInfo>(Url + Resources.InfoUrlSuffix);
        }

      
    }
}
