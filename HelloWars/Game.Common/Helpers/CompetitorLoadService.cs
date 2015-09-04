using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models;

namespace Common.Helpers
{
    public class CompetitorLoadService
    {
        public async Task<ICompetitor> LoadCompetitorAsync(string baseUrl)
        {
            var competitor = await WebClientHelper.GetDataAsync<Competitor>(baseUrl + Resources.InfoUrlSuffix);
            competitor.Url = baseUrl;
            competitor.Id = Guid.NewGuid();

            return competitor;
        }
    }
}
