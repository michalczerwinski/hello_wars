using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models;

namespace Common.Helpers
{
    public class CompetitorLoadService
    {
        public async Task<ICompetitor> LoadCompetitorAsync(string baseUrl, string gameType)
        {
            var competitor = await WebClientHelper.GetDataAsync<CompetitorInfo>(baseUrl + Resources.InfoUrlSuffix);
            
            if (competitor.GameType != gameType)
                return null;

            competitor.Url = baseUrl;
            competitor.Id = Guid.NewGuid();
            

            return competitor;
        }
    }
}
