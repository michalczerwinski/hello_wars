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

        public async Task<bool> VerifyAsync(string gameType)
        {
            IsVerified = false;
            var competitorInfo = await LoadCompetitorAsync();

            if (competitorInfo.GameType != gameType)
            {
                return IsVerified;
            }

            Name = competitorInfo.Name;
            AvatarUrl = competitorInfo.AvatarUrl;
            IsVerified = true;
            return IsVerified;
        }

        private async Task<CompetitorInfo> LoadCompetitorAsync()
        {
            var competitor = await WebClientHelper.GetDataAsync<CompetitorInfo>(Url + Resources.InfoUrlSuffix);

            competitor.Url = Url;

            return competitor;
        }
    }
}
