using System;
using Common.Interfaces;
using Common.Models;

namespace Common.Helpers
{
    public class CompetitorLoadService
    {
        private const string _infoUrl = "info";

        public ICompetitor LoadCompetitor(string baseUrl)
        {
            var competitor = WebClientHelper.GetData<Competitor>(baseUrl + _infoUrl);
            competitor.Url = baseUrl;
            competitor.Id = Guid.NewGuid();

            return competitor;
        }
    }
}
