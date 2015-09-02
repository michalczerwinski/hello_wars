using System.Net;

namespace Arena.Helpers
{
    public static class WebClientHelper
    {
        public static string GetStringResponseFromUrl(string url)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Accept", "application/json");
            return webClient.DownloadString(url);
        }
    }
}
