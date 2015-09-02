using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using SampleWebBotClient.Models;

namespace SampleWebBotClient.Controllers
{
    public class SampleBotController : ApiController
    {
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);

        [HttpGet]
        public BotClient Info()
        {
            var bot = new BotClient
            {
                Url = "",
                Name = "Czesiek",
                AvatarUrl = "http://localhost:53886/samplebot/avatar"
            };

            return bot;
        }

        [HttpGet]
        public HttpResponseMessage Avatar()
        {
            var path = HostingEnvironment.MapPath("~/Content/BotImg.png");
            var fileStream = new FileStream(path, FileMode.Open);
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new StreamContent(fileStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return result;
        }

        [HttpGet]
        public Point PerformNextMove()
        {
            return new Point
            {
                X = _rand.Next(0, 3),
                Y = _rand.Next(0, 3),
            };
        }
    }
}
