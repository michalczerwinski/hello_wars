using System.Net;

namespace Arena.Helpers
{
    public static class WebClientHelper<T>
    {
        public static T GetResponseFromUrl(string url)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Accept", "application/json");
            var downloadedString = webClient.DownloadString(url);

            return JsonHelper<T>.Deserialize(downloadedString);
        }
        }
}
